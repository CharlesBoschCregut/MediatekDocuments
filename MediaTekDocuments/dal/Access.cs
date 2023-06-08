using System;
using System.Collections.Generic;
using MediaTekDocuments.model;
using MediaTekDocuments.manager;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Configuration;
using Serilog;

namespace MediaTekDocuments.dal
{
    /// <summary>
    /// Classe d'accès aux données
    /// </summary>
    public class Access
    {
        /// <summary>
        /// adresse de l'API
        /// </summary>
        private static readonly string uriApi = ConfigurationManager.AppSettings["Apiurl"];
        /// <summary>
        /// instance unique de la classe
        /// </summary>
        private static Access instance = null;
        /// <summary>
        /// instance de ApiRest pour envoyer des demandes vers l'api et recevoir la réponse
        /// </summary>
        private readonly ApiRest api = null;
        /// <summary>
        /// méthode HTTP pour select
        /// </summary>
        private const string GET = "GET";
        /// <summary>
        /// méthode HTTP pour insert
        /// </summary>
        private const string POST = "POST";

        /// <summary>
        /// Méthode privée pour créer un singleton
        /// initialise l'accès à l'API
        /// </summary>
        private Access()
        {
            try
            {
                Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.File("logs/log.txt")
                .CreateLogger();
                api = ApiRest.GetInstance(uriApi, GetConnectionLogs());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Log.Fatal("Acces.cs catch GetConnectionLogs()={0}", GetConnectionLogs());
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// Récupération de la chaîne de connexion
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private static string GetConnectionLogs()
        {
            string username = ConfigurationManager.AppSettings["Username"];
            string password = ConfigurationManager.AppSettings["Password"];

            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                return $"{username}:{password}";
            }
            return "";
        }

        /// <summary>
        /// Création et retour de l'instance unique de la classe
        /// </summary>
        /// <returns>instance unique de la classe</returns>
        public static Access GetInstance()
        {
            if(instance == null)
            {
                instance = new Access();
            }
            return instance;
        }

        /// <summary>
        /// Retourne tous les genres à partir de la BDD
        /// </summary>
        /// <returns>Liste d'objets Genre</returns>
        public List<Categorie> GetAllGenres()
        {
            IEnumerable<Genre> lesGenres = TraitementRecup<Genre>(GET, "genre");
            return new List<Categorie>(lesGenres);
        }

        /// <summary>
        /// Retourne tous les rayons à partir de la BDD
        /// </summary>
        /// <returns>Liste d'objets Rayon</returns>
        public List<Categorie> GetAllRayons()
        {
            IEnumerable<Rayon> lesRayons = TraitementRecup<Rayon>(GET, "rayon");
            return new List<Categorie>(lesRayons);
        }

        /// <summary>
        /// Retourne toutes les catégories de public à partir de la BDD
        /// </summary>
        /// <returns>Liste d'objets Public</returns>
        public List<Categorie> GetAllPublics()
        {
            IEnumerable<Public> lesPublics = TraitementRecup<Public>(GET, "public");
            return new List<Categorie>(lesPublics);
        }

        public List<Etat> GetAllEtats()
        {
            IEnumerable<Etat> lesEtats = TraitementRecup<Etat>(GET, "etat");
            return new List<Etat>(lesEtats);
        }

        /// <summary>
        /// Retourne toutes les livres à partir de la BDD
        /// </summary>
        /// <returns>Liste d'objets Livre</returns>
        public List<Livre> GetAllLivres()
        {
            List<Livre> lesLivres = TraitementRecup<Livre>(GET, "livre");
            return lesLivres;
        }

        /// <summary>
        /// Retourne toutes les dvd à partir de la BDD
        /// </summary>
        /// <returns>Liste d'objets Dvd</returns>
        public List<Dvd> GetAllDvd()
        {
            List<Dvd> lesDvd = TraitementRecup<Dvd>(GET, "dvd");
            return lesDvd;
        }

        /// <summary>
        /// Retourne toutes les revues à partir de la BDD
        /// </summary>
        /// <returns>Liste d'objets Revue</returns>
        public List<Revue> GetAllRevues()
        {
            List<Revue> lesRevues = TraitementRecup<Revue>(GET, "revue");
            return lesRevues;
        }
        
        public List<Suivi> GetAllSuivis()
        {
            List<Suivi> lesSuivis = TraitementRecup<Suivi>(GET, "suivi");
            return lesSuivis;
        }


        /// <summary>
        /// Retourne les exemplaires d'une revue
        /// </summary>
        /// <param name="idDocument">id de la revue concernée</param>
        /// <returns>Liste d'objets Exemplaire</returns>
        public List<Exemplaire> GetExemplairesRevue(string idDocument)
        {
            List<Exemplaire> lesExemplaires = TraitementRecup<Exemplaire>(GET, "exemplaire/" + idDocument);
            return lesExemplaires;
        }

        public List<CommandeDocument> GetCommande(string id)
        {
            List<CommandeDocument> Commandes;
            Commandes = TraitementRecup<CommandeDocument>(GET, "commande/" + id);
            return Commandes;
        }


        public Livre GetLivre(string id)
        {
            List<Livre> livres = TraitementRecup<Livre>(GET, "livre/" + id);
            if (livres.Count > 0)
            {
                return livres[0];
            }
            else
            {
                return null;
            }
            
        }
        /// <summary>
        /// ecriture d'un exemplaire en base de données
        /// </summary>
        /// <param name="exemplaire">exemplaire à insérer</param>
        /// <returns>true si l'insertion a pu se faire (retour != null)</returns>
        public bool CreerExemplaire(Exemplaire exemplaire)
        {
            String jsonExemplaire = JsonConvert.SerializeObject(exemplaire, new CustomDateTimeConverter());
            try {
                // récupération soit d'une liste vide (requête ok) soit de null (erreur)
                List<Exemplaire> liste = TraitementRecup<Exemplaire>(POST, "exemplaire/" + jsonExemplaire);
                return (liste != null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Log.Error("Access.cs CreerExemplaire catch error={0}", ex.Message);
            }
            return false; 
        }

        public bool CreerDocument(Document document)
        {
            string json = JsonConvert.SerializeObject(document);
            try
            {
                List<Document> liste = TraitementRecup<Document>(POST, "document/" + "true/" + json);
                return (liste != null);
            } 
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
                Log.Error("Access.cs CreerDocument catch error={0}", ex.Message);
            }
            return false;
        }

        public bool CreerLivreDvd(LivreDvd ldvd)
        {
            string json = JsonConvert.SerializeObject(ldvd);
            try
            {
                List<LivreDvd> liste = TraitementRecup<LivreDvd>(POST, "livresdvd/" + "true/" + json);
                return (liste != null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Log.Error("Access.cs CreerLivreDvd catch error={0}", ex.Message);
            }
            return false;
        }

        public int GetPlusGrandId(string table) 
        {
            List<Dictionary<string, string>> ids = TraitementRecup<Dictionary<string, string>>(GET, table + "/get");
            return int.Parse(ids[0]["id"]);
        }

        public string GenererId(string table)
        {
            string res = "";
            int id = GetPlusGrandId(table);
            if (id > 0)
            {
                id += 1;
                res = id.ToString("D5");
            }
            else
            {
                switch (table)
                {
                    case "livre":
                        res = "00001";
                        break;
                    case "dvd":
                        res = "20001";
                        break;
                    case "revue":
                        res = "10001";
                        break;
                    case "commande":
                        res = "30001";
                        break;
                }
            }
            return res;
        }

        public bool CreerLivre(Livre livre) 
        {
            string json = JsonConvert.SerializeObject(livre);
            try
            {
                List<Livre> liste = TraitementRecup<Livre>(POST, "livre/" + "true/" + json);
                return (liste != null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Log.Error("Access.cs CreerLivre catch error={0}", ex.Message);
            }
            return false;
        }

        public bool EditerDocument(Document document)
        {
            string json = JsonConvert.SerializeObject(document);
            try
            {
                List<Document> liste = TraitementRecup<Document>(POST, "document/" + document.Id + "/UPD/" + json);
                return (liste != null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Log.Error("Access.cs EditerDocument catch error={0}", ex.Message);
            }
            return false;
        }

        public bool EditerLivre(Livre livre)
        {
            string json = JsonConvert.SerializeObject(livre);
            try
            {
                List<Livre> liste = TraitementRecup<Livre>(POST, "livre/" + livre.Id +"/UPD/" + json);
                return (liste != null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Log.Error("Access.cs EditerLivre catch error={0}", ex.Message);
            }
            return false;
        }

        public bool PeutSuppr(string table, string id)
        {
            try
            {
                List<Document> liste = TraitementRecup<Document>(GET, table + "/" + id);
                return (liste.Count <= 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Log.Error("Access.cs PeutSuppr catch error={0}", ex.Message);
            }
            return false;

        }

        public void Suppr(string type, string id)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>
            {
                { "id", id }
            };
            string json = JsonConvert.SerializeObject(dic);
            try
            {
                switch (type)
                {
                    case "livre":
                        TraitementRecup<Document>(POST, "livre/true/REM/" + json);
                        TraitementRecup<Document>(POST, "livresdvd/true/REM/" + json);
                        TraitementRecup<Document>(POST, "document/true/REM/" + json);
                        break;

                    case "dvd":
                        TraitementRecup<Document>(POST, "dvd/true/REM/" + json);
                        TraitementRecup<Document>(POST, "livresdvd/true/REM/" + json);
                        TraitementRecup<Document>(POST, "document/true/REM/" + json);
                        break;

                    case "revue":
                        TraitementRecup<Document>(POST, "revue/true/REM/" + json);
                        TraitementRecup<Document>(POST, "document/true/REM/" + json);
                        break;
                    case "commande":
                        TraitementRecup<Document>(POST, "commande/true/REM/" + json);
                        break;
                    case "exemplaire":
                        dic.Add("numero", id);
                        dic.Remove("id");
                        json = JsonConvert.SerializeObject(dic);
                        TraitementRecup<Exemplaire>(POST, "exemplaire/true/REM/" + json);
                        break;
                    default:
                        break;

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public bool CreerDvd(Dvd dvd) 
        {
            string json = JsonConvert.SerializeObject(dvd);
            try
            {
                List<Dvd> liste = TraitementRecup<Dvd>(POST, "dvd/" + "true/" + json);
                return (liste != null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Log.Error("Access.cs CreerDvd catch error={0}", ex.Message);
            }
            return false;
        }
        
        public bool EditerDvd(Dvd dvd)
        {
            string json = JsonConvert.SerializeObject(dvd);
            try
            {
                List<Dvd> liste = TraitementRecup<Dvd>(POST, "dvd/" + dvd.Id + "/UPD/" + json);
                return (liste != null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Log.Error("Access.cs EditerDvd catch error={0}", ex.Message);
            }
            return false;
        }

        public bool CreerRevue(Revue revue)
        {
            string json = JsonConvert.SerializeObject(revue);
            try
            {
                List<Revue> liste = TraitementRecup<Revue>(POST, "revue/" + "true/" + json);
                return (liste != null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Log.Error("Access.cs CreerRevue catch error={0}", ex.Message);
            }
            return false;
        }

        public bool EditerRevue(Revue revue)
        {
            string json = JsonConvert.SerializeObject(revue);
            try
            {
                List<Revue> liste = TraitementRecup<Revue>(POST, "revue/" + revue.Id + "/UPD/" + json);
                return (liste != null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Log.Error("Access.cs EditerRevue catch error={0}", ex.Message);
            }
            return false;
        }

        public bool CreerCommande(Commande commande)
        {
            string json = JsonConvert.SerializeObject(commande);
            try
            {
                List<Commande> liste = TraitementRecup<Commande>(POST, "commande/" + "true/" + json);
                return (liste != null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Log.Error("Access.cs CreerCommande catch error={0}", ex.Message);
            }
            return false;
        }

        public bool CreerCommandeDocument(CommandeDocument commande)
        {
            string json = JsonConvert.SerializeObject(commande);
            try
            {
                List<CommandeDocument> liste = TraitementRecup<CommandeDocument>(POST, "commandedocument/" + "true/" + json);
                return (liste != null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Log.Error("Access.cs CreerCommandeDocument catch error={0}", ex.Message);
            }
            return false;
        }
        
        public bool EditerCommandeDocument(CommandeDocument commandeDocument)
        {
            string json = JsonConvert.SerializeObject(commandeDocument);
            try
            {
                List<CommandeDocument> liste = TraitementRecup<CommandeDocument>(POST, "commandedocument/" + commandeDocument.Id + "/UPD/" + json);
                return (liste != null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Log.Error("Access.cs EditerCommandeDocument catch error={0}", ex.Message);
            }
            return false;
        }
        
        public bool EditerCommande(Commande commande)
        {
            string json = JsonConvert.SerializeObject(commande);
            try
            {
                List<Commande> liste = TraitementRecup<Commande>(POST, "commande/" + commande.Id + "/UPD/" + json);
                return (liste != null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Log.Error("Access.cs EditerCommande catch error={0}", ex.Message);
            }
            return false;
        }
        
        public bool EditerEtatExemplaire(Exemplaire exemplaire)
        {
            string json = JsonConvert.SerializeObject(exemplaire);
            try
            {
                List<Exemplaire> liste = TraitementRecup<Exemplaire>(POST, "exemplaire/" + exemplaire.Numero + "/UPD/" + json);
                return (liste != null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Log.Error("Access.cs EditerEtatExemplaire catch error={0}", ex.Message);
            }
            return false;
        }

        public string GetUserLogin(string login, string pwd)
        {
            string json = JsonConvert.SerializeObject(new Dictionary<string, string>
            {
                { "login", login },
                { "pwd", HashPassword(pwd) }
            });

            try
            {
                List<Dictionary<string, string>> liste = TraitementRecup<Dictionary<string, string>>(GET, "utilisateurs/" + "LOGIN/" + "LOGIN/" + json);
                if (liste.Count > 0)
                {
                    return liste[0]["idservice"];
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Log.Error("Access.cs GetUserLogin catch error={0}", ex.Message);
            }
            return "failed";
        }

        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    sb.Append(hash[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }
        /// <summary>
        /// Traitement de la récupération du retour de l'api, avec conversion du json en liste pour les select (GET)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="methode">verbe HTTP (GET, POST, PUT, DELETE)</param>
        /// <param name="message">information envoyée</param>
        /// <returns>liste d'objets récupérés (ou liste vide)</returns>
        private List<T> TraitementRecup<T> (String methode, String message)
        {
            List<T> liste = new List<T>();
            try
            {
                JObject retour = api.RecupDistant(methode, message);
                // extraction du code retourné
                String code = (String)retour["code"];
                if (code.Equals("200"))
                {
                    // dans le cas du GET (select), récupération de la liste d'objets
                    if (methode.Equals(GET))
                    {
                        String resultString = JsonConvert.SerializeObject(retour["result"]);
                        // construction de la liste d'objets à partir du retour de l'api
                        liste = JsonConvert.DeserializeObject<List<T>>(resultString, new CustomBooleanJsonConverter());
                    }
                }
                else
                {
                    Console.WriteLine("code erreur = " + code + " message = " + (String)retour["message"]);
                    Log.Error("Access.cs TraitementRecup catch code={0} message={1}", code, (String)retour["message"]);
                }
            }catch(Exception e)
            {
                Console.WriteLine("Erreur lors de l'accès à l'API : "+e.Message);
                Log.Fatal("Access.cs TraitementRecup catch error={0}", e.Message);
                Environment.Exit(0);
            }
            return liste;
        }

        /// <summary>
        /// Modification du convertisseur Json pour gérer le format de date
        /// </summary>
        private sealed class CustomDateTimeConverter : IsoDateTimeConverter
        {
            public CustomDateTimeConverter()
            {
                base.DateTimeFormat = "yyyy-MM-dd";
            }
        }

        /// <summary>
        /// Modification du convertisseur Json pour prendre en compte les booléens
        /// classe trouvée sur le site :
        /// https://www.thecodebuzz.com/newtonsoft-jsonreaderexception-could-not-convert-string-to-boolean/
        /// </summary>
        private sealed class CustomBooleanJsonConverter : JsonConverter<bool>
        {
            public override bool ReadJson(JsonReader reader, Type objectType, bool existingValue, bool hasExistingValue, JsonSerializer serializer)
            {
                return Convert.ToBoolean(reader.ValueType == typeof(string) ? Convert.ToByte(reader.Value) : reader.Value);
            }

            public override void WriteJson(JsonWriter writer, bool value, JsonSerializer serializer)
            {
                serializer.Serialize(writer, value);
            }
        }

    }
}
