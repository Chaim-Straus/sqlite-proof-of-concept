// Chaim Straus

namespace yoc_csharp_banking
{
    class PopulateData
        // populated default data into the database. not much else.
    {
        public static void populateDefaults()
        {
            main.CreateUpdateOrDelete("DROP TABLE bankData");
            main.CreateUpdateOrDelete("CREATE TABLE IF NOT EXISTS bankData (id integer primary key autoincrement, name text, password text, balance integer)");
            main.CreateUpdateOrDelete($"INSERT INTO bankData (id, name, password) VALUES (-1, 'admin', '{encryptSHA256.encrypt("admin")}')");
            main.CreateUpdateOrDelete($"INSERT INTO bankData (name, password, balance) VALUES ('Chaim Straus', '{encryptSHA256.encrypt("chaim")}', 100000)");
            main.CreateUpdateOrDelete($"INSERT INTO bankData (name, password, balance) VALUES ('Michael Roberts', '{encryptSHA256.encrypt("michael")}', 99999)");
            main.CreateUpdateOrDelete($"INSERT INTO bankData (name, password, balance) VALUES ('Shalom Feuer', '{encryptSHA256.encrypt("shalom")}', 99998)");
        }
    }
}
