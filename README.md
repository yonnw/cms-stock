SqlConnection con = new SqlConnection("Server=localhost\\SQLEXPRESS;Database=cmsstock;Uid=sa;Pwd=12345678");
con.Open();
SqlTransaction transaction = con.BeginTransaction();
var sql = "UPDATE INTO CentroCustos(fechada)values(1)";
SqlCommand cmd = new SqlCommand(sql, con, transaction);


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
- Fazer função equivalente ao ROR "aftersave" or "beforesave"
- Segurança da aplicação e rotas admin e user