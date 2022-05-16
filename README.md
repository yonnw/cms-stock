SqlConnection con = new SqlConnection("Server=localhost\\SQLEXPRESS;Database=cmsstock;Uid=sa;Pwd=12345678");
con.Open();
SqlTransaction transaction = con.BeginTransaction();
var sql = "UPDATE INTO CentroCustos(fechada)values(1)";
SqlCommand cmd = new SqlCommand(sql, con, transaction);


Situações a resolver:

Database:
Centro Custo criar valor de venda

Views:
Details - Art Centro Custos acrescentar campos
Resolver Art Centro Custos - procura
	Acrescentar procura por centro de custo
Criar detalhes art centro custos sem o campo valor para os utilizadores sem permissões - Validação pelos cookies
Alterar os logotipos do Quixlab
Art Centro Custos Index - Colocar a unidade

Validações:


WARNINGS / NEW INSTALLATIONS:
Validar o ID da referencia SERVIÇOS e alterar a incrementação que está na função (ArtCentroCustos-Create) line 86