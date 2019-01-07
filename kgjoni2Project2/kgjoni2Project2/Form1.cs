//
//  Windows  Form  App  to  allow  bike  rentals  via  the  BikeHike  database.
//
//  Kristi Gjoni
//  U.  of  Illinois,  Chicago
//  CS480,  Summer  2018
//  Project  #2
//

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace kgjoni2Project2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string filename, connectionInfo;
            SqlConnection db;

            filename = "BikeHike.mdf";

            string version;
            version = "MSSQLLocalDB";

            connectionInfo = String.Format(@"Data Source=(LocalDB)\{0};AttachDbFilename=|DataDirectory|\{1};Integrated Security=True;"
                                           , version, filename);

            db = new SqlConnection(connectionInfo);
            db.Open();

            // Retrieve values
            string sql = string.Format(@"SELECT CID, LastName, FirstName 
                                         FROM Customer 
                                         ORDER BY LastName ASC, Firstname ASC");

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = db;
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            cmd.CommandText = sql;
            adapter.Fill(ds);
            db.Close();

            this.listBox1.Items.Clear();

            // Display
            foreach (DataRow row in ds.Tables["TABLE"].Rows)
            {
                string msg = string.Format("{0}, {1}",
                    Convert.ToString(row["LastName"]),
                    Convert.ToString(row["FirstName"]));
                this.listBox1.Items.Add(msg);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string filename, connectionInfo;
            SqlConnection db;

            filename = "BikeHike.mdf";

            string version;
            version = "MSSQLLocalDB";

            connectionInfo = String.Format(@"Data Source=(LocalDB)\{0};AttachDbFilename=|DataDirectory|\{1};Integrated Security=True;"
                                           , version, filename);

            db = new SqlConnection(connectionInfo);
            db.Open();

            // Retrieve values
            string sql = string.Format(@"SELECT Bid 
                                         FROM Bike 
                                         ORDER BY Bid ASC");

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = db;
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            cmd.CommandText = sql;
            adapter.Fill(ds);
            db.Close();

            this.listBox2.Items.Clear();

            // Display
            foreach (DataRow row in ds.Tables["TABLE"].Rows)
            {
                string msg = string.Format("{0}",
                    Convert.ToString(row["Bid"]));
                this.listBox2.Items.Add(msg);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string filename, connectionInfo, version, username;
            SqlConnection db;

            filename = "BikeHike.mdf";
            username = this.listBox1.Text;
            version = "MSSQLLocalDB";

            connectionInfo = String.Format(@"Data Source=(LocalDB)\{0};AttachDbFilename=|DataDirectory|\{1};Integrated Security=True;"
                                           , version, filename);

            db = new SqlConnection(connectionInfo);
            db.Open();

            // Retireve values
            string sql = string.Format(@"SELECT Cid, Email
                                         FROM Customer
                                         WHERE LastName + ', ' + FirstName = '{0}'", username);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = db;
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            cmd.CommandText = sql;
            adapter.Fill(ds);
            db.Close();

            // Display
            foreach (DataRow row in ds.Tables["TABLE"].Rows)
            {
                string msg = string.Format("{0}  {1}",
                    this.textBox1.Text = Convert.ToString(row["CID"]),
                    this.textBox2.Text = Convert.ToString(row["EMAIL"]));
            }

            this.textBox3.Text = "Not on a rental";


            db = new SqlConnection(connectionInfo);
            db.Open();

            // Retrieve values
            string sql2 = string.Format(@"SELECT Rid
                                          FROM Rentals
                                          WHERE Cid = '{0}' AND ActualDuration IS NULL", this.textBox1.Text);

            cmd = new SqlCommand();
            cmd.Connection = db;
            cmd.CommandText = sql;
            object result = cmd.ExecuteScalar();
            db.Close();

            if (result.ToString() == " ")
                this.textBox3.Text = "not rented";
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

            string bike_id = this.listBox2.Text;

            string filename, connectionInfo, version;
            SqlConnection db;

            filename = "BikeHike.mdf";
            version = "MSSQLLocalDB";

            connectionInfo = String.Format(@"Data Source=(LocalDB)\{0};AttachDbFilename=|DataDirectory|\{1};Integrated Security=True;"
                                           , version, filename);

            db = new SqlConnection(connectionInfo);
            db.Open();

            // Retrieve values
            string sql = string.Format(@"SELECT ServiceYear, Description, PricePerHour 
                                         From Bike
                                         INNER JOIN Byke_Type ON Bike.Btid = Byke_Type.Btid
                                         WHERE Bid = '{0}' AND Rented IS NULL", bike_id);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = db;
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            cmd.CommandText = sql;
            adapter.Fill(ds);
            db.Close();

            // Display
            foreach (DataRow row in ds.Tables["TABLE"].Rows)
            {
                string msg = string.Format("{0} {1} {2}",
                    this.textBox4.Text = Convert.ToString(row["ServiceYear"]),
                    this.textBox5.Text = Convert.ToString(row["Description"]),
                    this.textBox6.Text = Convert.ToString(row["PricePerHour"]));
            }

            this.textBox7.Text = "Not rented";

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string filename, connectionInfo;
            SqlConnection db;

            filename = "BikeHike.mdf";

            string version;
            version = "MSSQLLocalDB";

            connectionInfo = String.Format(@"Data Source=(LocalDB)\{0};AttachDbFilename=|DataDirectory|\{1};Integrated Security=True;"
                                           , version, filename);

            db = new SqlConnection(connectionInfo);
            db.Open();

            // Retrieve values
            string sql = string.Format(@"SELECT Bid, Description
                                         FROM Bike
                                         INNER JOIN Byke_Type ON Bike.Btid = Byke_Type.Btid
                                         GROUP BY Description, ServiceYear, Bid
                                         ORDER BY Description ASC, ServiceYear DESC, Bid DESC");

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = db;
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            cmd.CommandText = sql;
            adapter.Fill(ds);
            db.Close();

            this.listBox3.Items.Clear();

            // Display
            foreach (DataRow row in ds.Tables["TABLE"].Rows)
            {
                string msg = string.Format("{0}: {1}",
                    Convert.ToString(row["BID"]),
                    Convert.ToString(row["Description"]));
                this.listBox3.Items.Add(msg);
            }
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            // no change
        }

        private void button4_Click(object sender, EventArgs e)
        {

            string filename, connectionInfo;
            SqlConnection db, dt, ds, dx, dy;

            filename = "BikeHike.mdf";

            string version;


            version = "MSSQLLocalDB";

            connectionInfo = String.Format(@"Data Source=(LocalDB)\{0};AttachDbFilename=|DataDirectory|\{1};Integrated Security=True;"
                                           , version, filename);


            // Check if customer is selected
            if (this.textBox1.Text == "")
            {
                MessageBox.Show("Select a customer..");
                return;
            }

            // Check if bike id is entered
            if (this.textBox8.Text == "")
            {
                MessageBox.Show("Enter one Bike ID..");
                return;
            }

            // Check if expected duration is entered
            if (this.textBox10.Text == "")
            {
                MessageBox.Show("Enter expected duration..");
                return;
            }

            // Check if total # of bikes is entered
            if (this.textBox9.Text == "")
            {
                MessageBox.Show("Enter total # of bikes..");
                return;
            }

            string customer_id = this.textBox1.Text.ToString();

            // Check if bike is rented or customer is out on a rental
            // NOT SURE WHY IT DOES NOT WORK

            /*dy = new SqlConnection(connectionInfo);
            dy.Open();

            string SQL = string.Format(@" SELECT Rented FROM Bike
                                          INNER JOIN RentalDetail ON Bike.Bid = RentalDetail.Bid
                                          INNER JOIN Rental ON RentalDetail.Rid = Rental.Rid
                                          WHERE Cid = '{0}'", customer_id);

            SqlCommand cm = new SqlCommand();
            cm.Connection = dy; 
            cm.CommandText = SQL;
            object r = cm.ExecuteScalar();
            dy.Close();

            if(r != null)
            {
                MessageBox.Show("Bike is rented");
                return;
            }
            */

            int bikes = System.Int32.Parse(this.textBox8.Text);
            double expectedDuration = Convert.ToDouble(this.textBox9.Text.ToString());            
            string total_bikes = this.textBox10.Text.ToString();

            db = new SqlConnection(connectionInfo);
            db.Open();

            // Insert values in Rental
            string sql = string.Format(@"   INSERT INTO 
                                            Rental(Cid, StartTime, ExpDuration, NumBikes, ActDuration, TotalPrice) 
                                            Values({0}, GetDate(), {1}, {2}, NULL, NULL)", customer_id, expectedDuration, total_bikes);


            SqlCommand cmd = new SqlCommand(); cmd.Connection = db; cmd.CommandText = sql; int rowsModified = cmd.ExecuteNonQuery(); db.Close();

            MessageBox.Show("Success");


            dx = new SqlConnection(connectionInfo);
            dx.Open();

            // Retrieve RID 
            string sql4 = string.Format(@"  SELECT Rid 
                                            FROM Rental
                                            WHERE CID = '{0}' AND ExpDuration = '{1}' AND NumBikes = '{2}'", customer_id, expectedDuration, total_bikes);

            SqlCommand cmd3 = new SqlCommand();
            cmd3.Connection = dx;
            cmd3.CommandText = sql4;
            object result = cmd3.ExecuteScalar();
            string rid = Convert.ToString(result);
            dx.Close();

            dt = new SqlConnection(connectionInfo);
            dt.Open();

            // Insert values in RentalDetail
            string sql2 = string.Format(@"  INSERT INTO 
                                            RentalDetail(Rid, Bid)
                                            Values({0}, {1})", rid, bikes);

            SqlCommand cmd1 = new SqlCommand();
            cmd1.Connection = dt;
            cmd1.CommandText = sql2;
            cmd1.ExecuteNonQuery();
            dt.Close();

            ds = new SqlConnection(connectionInfo);
            ds.Open();

            // Update Rented to T
            string sql3 = string.Format(@"  UPDATE Bike
                                            SET Rented = 'T'
                                            WHERE Bid = {0}", bikes);

            SqlCommand cmd2 = new SqlCommand();
            cmd2.Connection = ds;
            cmd2.CommandText = sql3;
            cmd2.ExecuteNonQuery();
            ds.Close();
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            // no change
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // Check if customer is selected
            if(this.textBox1.Text == "")
            {
                MessageBox.Show("Select a customer..");
                return;
            }

            string customer_id = this.textBox1.Text.ToString();
            string filename, connectionInfo;
            SqlConnection db, dt, dx, ds;

            filename = "BikeHike.mdf";

            string version;
            version = "MSSQLLocalDB";

            connectionInfo = String.Format(@"Data Source=(LocalDB)\{0};AttachDbFilename=|DataDirectory|\{1};Integrated Security=True;"
                                           , version, filename);

            db = new SqlConnection(connectionInfo);
            db.Open();

            // Retrieve Start Time
            string sql = string.Format(@"SELECT StartTime FROM Rental
                                         WHERE Cid = '{0}'", customer_id);

            SqlCommand cmd3 = new SqlCommand();
            cmd3.Connection = db;
            cmd3.CommandText = sql;
            object result = cmd3.ExecuteScalar();
            string startTime = Convert.ToString(result);

            db.Close();

            dt = new SqlConnection(connectionInfo);
            dt.Open();

            // Retrieve the actual duration
            string sql2 = string.Format(@"SELECT DATEDIFF(hour, '{0}', GetDate())", startTime);

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = dt;
            cmd.CommandText = sql2;
            object result2 = cmd.ExecuteScalar();
            string actualTime = Convert.ToString(result2);
            dt.Close();

            dx = new SqlConnection(connectionInfo);
            dx.Open();

            // Retrive price per hour
            string sql3 = string.Format(@"SELECT PricePerHour FROM Byke_Type
                                          INNER JOIN Bike ON Byke_Type.Btid = Bike.Btid
                                          INNER JOIN RentalDetail ON Bike.Bid = RentalDetail.Bid
                                          INNER JOIN Rental ON RentalDetail.Rid = Rental.Rid
                                          WHERE Cid = '{0}'", customer_id);

            SqlCommand cmd1 = new SqlCommand();
            cmd1.Connection = dx;
            cmd1.CommandText = sql3;
            object result3 = cmd1.ExecuteScalar();
            double price = Convert.ToDouble(result3);
            
            dx.Close();

            // Calculate total price
            double totalPrice = price * (Convert.ToDouble(actualTime));


            ds = new SqlConnection(connectionInfo);
            ds.Open();

            // Update ActualDuration
            string sql4 = string.Format(@"  UPDATE Rental
                                            SET ActDuration = '{0}'
                                            WHERE Cid = '{1}'", actualTime, customer_id);

            // Update Total Price
            string sql5 = string.Format(@"  UPDATE Rental
                                            SET TotalPrice = '{0}'
                                            WHERE Cid = '{1}'", totalPrice, customer_id);

            SqlCommand cmd5 = new SqlCommand();
            cmd5.Connection = ds;
            cmd5.CommandText = sql4;
            cmd5.ExecuteNonQuery();

            SqlCommand cmd6 = new SqlCommand();
            cmd6.Connection = ds;
            cmd6.CommandText = sql5;
            cmd6.ExecuteNonQuery();

            ds.Close();

            // Display actual duration and total price
            MessageBox.Show("Actual Time: " + actualTime + " - Price: " + totalPrice);
        }
    }
}
