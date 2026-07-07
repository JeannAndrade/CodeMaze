# Sobre o método UseHsts()

## O que é HSTS

**HSTS** (HTTP Strict Transport Security) é uma política de segurança que instrui o **navegador do cliente** a nunca mais tentar acessar seu site via HTTP puro — só via HTTPS — mesmo que o usuário digite `http://` na barra de endereço ou clique em um link antigo apontando para HTTP.

## Como funciona na prática

Quando o servidor responde com o header:

```
Strict-Transport-Security: max-age=31536000; includeSubDomains
```

O navegador "memoriza" isso e, na próxima vez que o usuário tentar acessar aquele domínio (mesmo digitando `http://seusite.com`), o **próprio navegador reescreve a requisição para HTTPS antes mesmo de enviá-la pela rede** — nem chega a bater no servidor como HTTP.

## Por que isso importa

Sem HSTS, o fluxo normalmente é:

1. Cliente acessa `http://seusite.com`
2. Servidor responde com redirect 301/302 para `https://seusite.com`
3. Cliente refaz a requisição em HTTPS

O problema é que esse **primeiro request em HTTP** é vulnerável — um atacante numa rede insegura (Wi-Fi público, por exemplo) pode interceptar essa requisição inicial antes do redirect acontecer (ataque conhecido como *SSL stripping*). Com HSTS, essa janela de vulnerabilidade desaparece, porque o navegador nunca chega a mandar a requisição em HTTP.

## Configuração típica

```csharp
if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
```

Você pode customizar as opções no `ConfigureServices`/`builder.Services`:

```csharp
builder.Services.AddHsts(options =>
{
    options.MaxAge = TimeSpan.FromDays(365);
    options.IncludeSubDomains = true;
    options.Preload = true;
});
```

## Pontos de atenção

- **`app.UseHsts()` normalmente não é usado em desenvolvimento.** Por isso o livro (e o template padrão do ASP.NET Core) costuma condicionar isso com `if (!app.Environment.IsDevelopment())`. Em dev, você geralmente usa certificado autoassinado e HTTP local, e HSTS causaria dor de cabeça (o navegador passaria a forçar HTTPS até em `localhost`, e isso "gruda" no cache do navegador por muito tempo).
- **`UseHsts()` não substitui `UseHttpsRedirection()`** — são complementares. O `UseHttpsRedirection()` faz o redirect 307/308 do lado do servidor; o `UseHsts()` garante que, depois da primeira visita, o navegador nem tente HTTP de novo.
- **O header só tem efeito após o navegador já ter recebido pelo menos uma resposta HTTPS com esse header** — ou seja, não protege o *primeiríssimo* acesso, apenas os subsequentes (por isso existem as *preload lists* dos navegadores, para domínios que querem proteção desde o primeiro acesso).
