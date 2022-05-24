Situações a resolver:

Database:

Views:
Alterar os logotipos do Quixlab
Identificar os campos obrigatórios

Validações:
FuncCentrodeCustos - Se o ModelState for invalid mandar o erro para Index - Centro Custos
EquiCentrodeCustos - Se o ModelState for invalid mandar o erro para Index - Centro Custos
ArtCentroCustos - Data get data today
Criar secção de encomendas e adicionar o centro de custo ID, nome, Qtd


WARNINGS / NEW INSTALLATIONS:
Validar o ID da referencia SERVIÇOS e alterar a incrementação que está na função (ArtCentroCustos-Create) line 120 e 124
Validar o ID da referencia SERVIÇOS e alterar a incrementação que está na View - details - CentroCustos line 167


Danilo:
- Fazer função equivalente ao ROR "aftersave" or "beforesave", para os calculos efetuados em Edit e Create 
	- Active Record Callbacks
	- ArtigoServico.cs - IService , Interface c# avançado SOLID

- Segurança da aplicação e rotas admin e user ?
	- IONIC Modulo 13

- Trabalhar com database do PHC, aplicação 3 camadas ?
	- Mesma instancia SQL 
	- Duplicar registos ou fazer querys diretas ?
	- DDD

- Para gerar PDFs ?
	- https://www.torneseumprogramador.com.br/aula?id=B3VGTzUHRMc&aula=83&tipo=c-sharp&professor=Danilo

- Modelstate.IsValid 
	- Não valida todos os campos required do Modelo, é boa prática usar?

- Auditoria de base de dados ? 