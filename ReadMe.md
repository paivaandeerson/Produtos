# Crud de Produtos
Este documento provê uma visão geral da implementação facilitando o entendimento com base nas decisões tomadas de forma consciente.

## Índice
- [Run App](#R)
- [Complexidade Acidental](#C)
- [SOLID](#S)
- [Perfomance](#P)
- [Features](#P)
- [Tempo de implementação](#T)

## <a name="R">Run App</a>

1. Executar o script ``InitDB.sql`` presente neste diretório  no SqlServer antes da execução do App, garanta que a ``ConnectionString`` em ``src\Anderson.Produtos.WebAPI\appSettings.Development.json`` está apontando corretamente para Base de Dados de sua escolha.
2. Para rodar o app é necessário ter o VS2019 16.8.2 ou VSCode + CLI
3. Em ambiente de desenvolvimento utilizei ``npm 6.x`` e ``node 14.x`` (versões mais recentes podem apresentar erros)

## <a name="C">Complexidade Acidental</a>
O motivo da complexidade desproporcional à demanda presente no Back-End foi para mostrar conhecimento e atender a solicitação do SOLID. Foi feito o desacoplamento da API Asp.net com o *Core* do App que somente expõe o ``Application`` e ``ViewModels`` para consumo seguindo o padrão de design *Arquitetura Hexagonal (Ports & Adapters)* e *MVVM*. Toda a junção em ``Anderson.Produtos.Domain``, foi para diminuir a complexidade e reduzir o tempo de implementação, vale ressaltar que a escolha desta complexidade em um Crud Real deveria ser repensada.

## <a name="S">SOLID</a>
Esta implementação não se baseou em todos, mas em alguns princípios do SOLID, os que ficaram de fora não se encaixaram dentro da demanda.

## <a name="P">Perfomance</a>
O App não possui problemas de perfomance (por motivos óbvios), contudo foram adotadas escolhas pensando na eficiência que me pareceram as melhores alternativas.

## <a name="F">Features</a>
O App faz uso de C#9 e EFCore 5, devido a este fato estão presentes no mesmo algumas das boas práticas utilizando as mais recentes features implementadas pela Microsoft.


## <a name="T">Tempo de implementação</a>
Devido ao tempo de implementação ser curto, foram utilizados *boilerplates* de projetos tantos pesssoais quanto encontrados pela internet.
