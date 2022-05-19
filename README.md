SqlConnection con = new SqlConnection("Server=localhost\\SQLEXPRESS;Database=cmsstock;Uid=sa;Pwd=12345678");
con.Open();
SqlTransaction transaction = con.BeginTransaction();
var sql = "UPDATE INTO CentroCustos(fechada)values(1)";
SqlCommand cmd = new SqlCommand(sql, con, transaction);


Situações a resolver:

Database:
Centro Custo criar valor de venda

Views:
Alterar os logotipos do Quixlab
Identificar os campos obrigatórios

Validações:
Art Centro Custo - criar view para user normal sem o campo valor
				 - criar index para user normal sem valores e btn apagar

WARNINGS / NEW INSTALLATIONS:
Validar o ID da referencia SERVIÇOS e alterar a incrementação que está na função (ArtCentroCustos-Create) line 120 e 124