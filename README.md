Acrescentar nos artigos o preço de custo e unidade.


                SqlConnection con = new SqlConnection("Server=localhost\\SQLEXPRESS;Database=cmsstock;Uid=sa;Pwd=12345678");
                con.Open();
                SqlTransaction transaction = con.BeginTransaction();
                var sql = "UPDATE INTO CentroCustos(fechada)values(1)";
                SqlCommand cmd = new SqlCommand(sql, con, transaction);