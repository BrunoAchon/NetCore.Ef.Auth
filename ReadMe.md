# .Net Core - Entity Framework - Dapper - Swagger - Solid

Este projeto, tem como princípio iniciar uma API de autenticação e gestão de usuários, controlando desde o login com JWT até a distribuição de claims de acesso para os usuarios de um sistema. esta API busca fornecer condição para qualquer sistema se autenticar e ter niveis de acesso desde a visualização até gravação, deleção e edição de registros dinamicamente.

## Swagger

Esta sendo usado o Swagger como ferramenta de documentação e visalização de End-points.

## Entity Framework & Dapper 

O intuito é ter uma possibilidade hibrida que consiga responder bem tanto com as ferramentas de auto nivel de segurança (EF) quanto com o Dapper para atividades mais triviais que nao exigem autenticação ou nao necessitam um controle rigido.

## Princípios Solid 

Este projeto esta sendo inteiramente projetado em cima dos principios SOLID com as seguintes finalidades:

* Tornar o código mais entendível, claro e conciso;
* Tornar o código mais flexível e tolerante a mudanças;
* Aumentar a adesão do código aos princípios da orientação a objetos.

Os princípios SOLID são cinco princípios de design de código orientado a objeto que basicamente tem os seguintes objetivos:

* Single Responsability Principle (Princípio da Responsabilidade Única);
* Open/Closed Principle (Princípio do “Aberto para Extensão/Fechado para Implementação);
* Liskov Substitution Principle (Princípio da Substituição de Liskov);
* Interface Segregation Principle (Princípio da Segregação de Interfaces);
* Dependency Inversion Principle (Princípio da Inversão de Dependências)

## Testes

Está sendo aplicado o padrão de testes Triple A (Arrange, Act, Assert), até o momento foram aplicados Testes Unitários com TDD (Test Driven Development), juntamente com DataFakes com Bogus.

###### Nota: Implantar: Test Doubles (Mocks, Stubs, Fakes, Spies, Dummies)
