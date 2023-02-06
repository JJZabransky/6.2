using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _6._2
{
    public class Database
    {
        public void Insert(Zamestnanec z)
        {
            SqlConnection conn = Connection.GetInstance();

            string query = "INSERT INTO zamestnanec (jmeno,prijmeni,vek) values(@jmeno,@prijmeni,@vek)";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.Add(new SqlParameter("@jmeno",z.Jmeno));
                cmd.Parameters.Add(new SqlParameter("@prijmeni", z.Prijmeni));
                cmd.Parameters.Add(new SqlParameter("@vek", z.Vek));
                cmd.ExecuteNonQuery();
                cmd.CommandText = "Select @@Identity";
                z.ID = Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        public void Update(Zamestnanec z) 
        {
            SqlConnection conn = Connection.GetInstance();

            string query = "UPDATE zamestnanec (jmeno,prijmeni,vek) values(@jmeno,@prijmeni,@vek)";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.Add(new SqlParameter("@jmeno", z.Jmeno));
                cmd.Parameters.Add(new SqlParameter("@prijmeni", z.Prijmeni));
                cmd.Parameters.Add(new SqlParameter("@vek", z.Vek));
                cmd.ExecuteNonQuery();
                cmd.CommandText = "Select @@Identity";
            }
        }

        public void Delete(Zamestnanec z)
        {
            SqlConnection conn = Connection.GetInstance();

            string query = "DELETE FROM zamestnanec WHERE id = @id";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.Add(new SqlParameter("@id", z.ID));
                cmd.ExecuteNonQuery();
                z.ID = 0;
            }
        }

        public IEnumerable<Zamestnanec> Select()
        {
            SqlConnection conn = Connection.GetInstance();

            string query = "SELECT * FROM zamestnanec";
            using(SqlCommand cmd = new SqlCommand(query, conn))
            {
                SqlDataReader rd = cmd.ExecuteReader();

                while(rd.Read())
                {
                    Zamestnanec z = new Zamestnanec(Convert.ToInt32(rd[0].ToString()), rd[1].ToString(), rd[2].ToString(), Convert.ToInt32(rd[3].ToString()));
                    yield return z;
                }
                rd.Close();
            }
        }


    }
}
