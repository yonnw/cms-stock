SqlConnection con = new SqlConnection("Server=localhost\\SQLEXPRESS;Database=cmsstock;Uid=sa;Pwd=12345678");
con.Open();
SqlTransaction transaction = con.BeginTransaction();
var sql = "UPDATE INTO CentroCustos(fechada)values(1)";
SqlCommand cmd = new SqlCommand(sql, con, transaction);


Situações a resolver:

Database:
Centro Custo criar valor de venda

Views:
Resolver Art Centro Custos - procura
	Acrescentar procura por centro de custo

Alterar os logotipos do Quixlab
ArtCentroCusto Index - colocar UNIDADE e linhas a vermelho caso o valor seja igual a 0.


Validações:
ArtCentroCusto - redirect apos criado com sucesso para admin e user , e redirect sem sucesso passar erro para admin e user

WARNINGS / NEW INSTALLATIONS:
Validar o ID da referencia SERVIÇOS e alterar a incrementação que está na função (ArtCentroCustos-Create) line 86