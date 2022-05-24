Situações a resolver:

Database:

Views:
Alterar os logotipos do Quixlab
Identificar os campos obrigatórios

Validações:
FuncCentrodeCustos - Se o ModelState for invalid mandar o erro para Index - Centro Custos
EquiCentrodeCustos - Se o ModelState for invalid mandar o erro para Index - Centro Custos


WARNINGS / NEW INSTALLATIONS:
Validar o ID da referencia SERVIÇOS e alterar a incrementação que está na função (ArtCentroCustos-Create) line 120 e 124
Validar o ID da referencia SERVIÇOS e alterar a incrementação que está na View - details - CentroCustos line 167


Danilo:
- Fazer função equivalente ao ROR "aftersave" or "beforesave", para os calculos efetuados em Edit e Create 
- Segurança da aplicação e rotas admin e user
- Trabalhar com database do PHC, aplicação 3 camadas?