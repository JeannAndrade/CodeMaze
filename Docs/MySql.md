# Sobre o uso do MySql

## Como criar o container local

O container do MySql deve ser criado adicionando as variáveis de ambiente que criarão o banco de dados e o usuário transacional.

```bash
docker run --name codemaze-local \
  -e MYSQL_ROOT_PASSWORD=$CODEMAZE_DB_ROOT \
  -e MYSQL_DATABASE=$CODEMAZE_DB_NAME \
  -e MYSQL_USER=$CODEMAZE_DB_USER \
  -e MYSQL_PASSWORD=$CODEMAZE_DB_PASS \
  -p 3306:3306 \
  -d mysql:lts
```

Criado o banco já com o usuário, já é possível atualizar o banco com o Migration.

## Verificando banco via docker

```bash
docker exec -it codemaze-local mysql -u$CODEMAZE_DB_USER -p$CODEMAZE_DB_PASS $CODEMAZE_DB_NAME
```

## Para exibir o resultado

Para ver as tabelas criadas:

`SHOW TABLES;`

Para ver as colunas de uma tabela:

`DESCRIBE MinhaTabela;`
