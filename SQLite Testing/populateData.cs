namespace SQLite_Testing
{
    class PopulateData
    {
        public static void populateDefaults()
        {
            main.CreateUpdateOrDelete("DROP TABLE testTable");
            main.CreateUpdateOrDelete("CREATE TABLE IF NOT EXISTS testTable (id integer primary key autoincrement, name text, password text, balance integer)");
            main.CreateUpdateOrDelete($"INSERT INTO testTable (name, password, balance) VALUES ('Chaim Straus', '{encryptSHA256.encrypt("f4d3s2a1")}', 100000)");
            main.CreateUpdateOrDelete($"INSERT INTO testTable (name, password, balance) VALUES ('Michael Roberts', '{encryptSHA256.encrypt("a1s2d3f4")}', 99999)");
            main.CreateUpdateOrDelete($"INSERT INTO testTable (name, password, balance) VALUES ('Shalom Feuer', '{encryptSHA256.encrypt("pityaccount")}', 99998)");
        }
    }
}
