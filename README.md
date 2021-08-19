# Sistema Programa de Gestão de Desempenho - PGD

Fork do [Sistema_Programa_de_Gestao_Susep](https://github.com/spbgovbr/Sistema_Programa_de_Gestao_Susep).

Para entender os conceitos e principais funcionalidades, assista à apresentação disponível no seguinte link: [youtu.be/VU_1TTAMg2Y](https://youtu.be/VU_1TTAMg2Y).

## Avaliação ou desenvolvimento

Pré-requisito: [**dotnet core sdk 3.1**](https://dotnet.microsoft.com/download/dotnet/3.1), não é necessário ao utilizar
apenas o ambiente docker.

As configurações do projeto estão ajustadas para permitir o desenvolvimento local no linux ou windows, com ou sem docker.
Porém, há configurações do docker para o banco de dados SQL Server para facilitar o desenvolvimento ou a avaliação do
sistema.

Para utilizar o banco de dados com docker, utilize o seguinte comando na raíz do projeto:

```sh
docker-compose up -d
```

Após a inicialização do banco de dados, os _scripts_ SQL no diretório `install` serão automaticamente executados.

O comando acima, inicializa todos os serviços do ambiente docker. Para acessar a aplicação do ambiente docker utilize a URL [http://localhost/safe/sisgp/programagestao/app/login](http://localhost/safe/sisgp/programagestao/app/login).

Para desenvolvimento local, sem docker, utilize os comandos abaixo:

```sh
dotnet run --project src/Susep.SISRH.WebApi/Susep.SISRH.WebApi.csproj
```

```sh
dotnet run --project src/Susep.SISRH.ApiGateway/Susep.SISRH.ApiGateway.csproj
```

```sh
dotnet run --project src/Susep.SISRH.WebApp/Susep.SISRH.WebApp.csproj
```

A solução compreende três projetos:

1. WebApp - [https://127.0.0.1:5001](https://127.0.0.1:5001) (acesso ao sistema)
2. WebApi - `http://127.0.0.1:5003`
3. ApiGateway - `http://127.0.0.1:5004`
