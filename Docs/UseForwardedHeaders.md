# Sobre o método app.UseForwardedHeaders()

## Pergunta

Estou estudando o livro do Code Maze, Ultimate ASP.NET Core Web API e me deparei com esse texto:

"app.UseForwardedHeaders() will forward proxy headers to the current request. This will help us during application deployment. Pay attention that we require Microsoft.AspNetCore.HttpOverrides using directive to introduce the ForwardedHeaders enumeration"

Não ficou claro para mim o que o método UseForwardedHeaders realmente faz.

## Respota

Boa pergunta — esse trecho do livro é bem resumido e realmente deixa a dúvida no ar. Vou explicar o que acontece de verdade por trás desse método.

### O problema que ele resolve

Quando sua aplicação ASP.NET Core roda atrás de um proxy reverso ou load balancer (Nginx, IIS, um Application Gateway do Azure, um Load Balancer da AWS, etc.), a requisição que chega no seu Kestrel **não é mais a requisição original do cliente**. Ela passou por um intermediário.

Isso causa um problema: o `HttpContext` da sua aplicação vai enxergar informações erradas, como:

- **`Request.Scheme`** aparecendo como `http`, mesmo que o cliente tenha acessado via `https` (porque o TLS foi terminado no proxy, e a conexão entre o proxy e sua app é HTTP puro).
- **`Request.Host`** mostrando o endereço interno do servidor, em vez do domínio público que o cliente realmente digitou.
- **`Connection.RemoteIpAddress`** mostrando o IP do proxy, não o IP real do cliente.

### Como o proxy contorna isso

Proxies bem configurados adicionam **headers HTTP especiais** na requisição antes de repassá-la para sua aplicação, informando os dados originais:

- `X-Forwarded-For` → IP original do cliente
- `X-Forwarded-Proto` → esquema original (`http` ou `https`)
- `X-Forwarded-Host` → host original

### O que `UseForwardedHeaders()` faz

Esse middleware **lê esses headers** (`X-Forwarded-For`, `X-Forwarded-Proto` etc.) e **sobrescreve** as propriedades correspondentes no `HttpContext` (`Connection.RemoteIpAddress`, `Request.Scheme`, `Request.Host`) com os valores corretos, antes que o resto do pipeline (autenticação, geração de URLs, redirecionamentos, etc.) processe a requisição.

Sem isso, coisas como redirecionamentos de HTTPS, geração de links absolutos, ou políticas baseadas em IP do cliente podem quebrar ou se comportar de forma inconsistente em produção.

### Exemplo típico de configuração

```csharp
using Microsoft.AspNetCore.HttpOverrides;

var forwardedHeadersOptions = new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
};

app.UseForwardedHeaders(forwardedHeadersOptions);
```

O `enum ForwardedHeaders` (por isso o `using Microsoft.AspNetCore.HttpOverrides`) define **quais** desses headers você quer que o middleware processe — você pode combinar com `|` (OR bit a bit) para aceitar vários tipos.

### Ponto de atenção importante

A ordem no pipeline importa muito: `UseForwardedHeaders()` deve vir **antes** de outros middlewares que dependem do esquema/host/IP corretos (como `UseHttpsRedirection`, autenticação, etc.).

Além disso, por padrão o ASP.NET Core só aceita esses headers vindos de proxies conhecidos (`KnownProxies`/`KnownNetworks`), como medida de segurança — senão qualquer cliente malicioso poderia forjar esses headers e enganar sua aplicação (fazendo spoofing de IP, por exemplo). Se seu proxy não estiver na lista de redes confiáveis, os headers são ignorados silenciosamente — é uma causa comum de "configurei tudo certo mas não funciona".

Faz mais sentido agora? Posso mostrar um exemplo prático de configuração para um cenário específico (Nginx, Docker, etc.) se quiser — pelo que você tem trabalhado ultimamente com Docker, pode ser bem relevante.
