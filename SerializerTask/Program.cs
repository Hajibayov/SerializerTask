using System;
using System.Text;
using System.Threading.Channels;

namespace ManualJsonSerialization
{
    class Program
    {
        static void Main(string[] args)
        {
            var employee = new
            {
                gender = "female",
                name = new
                {
                    title = "Miss",
                    first = "Jennie",
                    last = "Nichols"
                },
                location = new
                {
                    street = new
                    {
                        number = 8929,
                        name = "Valwood Pkwy"
                    },
                    city = "Billings",
                    state = "Michigan",
                    country = "United States",
                    postcode = "63104",
                    coordinates = new
                    {
                        latitude = "-69.8246",
                        longitude = "134.8719"
                    },
                    timezone = new
                    {
                        offset = "+9:30",
                        description = "Adelaide, Darwin"
                    }
                },
                email = "jennie.nichols@example.com",
                login = new
                {
                    uuid = "7a0eed16-9430-4d68-901f-c0d4c1c3bf00",
                    username = "yellowpeacock117",
                    password = "addison",
                    salt = "sld1yGtd",
                    md5 = "ab54ac4c0be9480ae8fa5e9e2a5196a3",
                    sha1 = "edcf2ce613cbdea349133c52dc2f3b83168dc51b",
                    sha256 = "48df5229235ada28389b91e60a935e4f9b73eb4bdb855ef9258a1751f10bdc5d"
                },
                dob = new
                {
                    date = "1992-03-08T15:13:16.688Z",
                    age = 30
                },
                registered = new
                {
                    date = "2007-07-09T05:51:59.390Z",
                    age = 14
                },
                phone = "(272) 790-0888",
                cell = "(489) 330-2385",
                id = new
                {
                    name = "SSN",
                    value = "405-88-3636"
                },
                picture = new
                {
                    large = "https://randomuser.me/api/portraits/men/75.jpg",
                    medium = "https://randomuser.me/api/portraits/med/men/75.jpg",
                    thumbnail = "https://randomuser.me/api/portraits/thumb/men/75.jpg"
                },
                nat = "US"
            };

            string jsonString = SerializeToJson(employee);

            Console.WriteLine(jsonString);
        }

        static string SerializeToJson(object obj)
        {
            StringBuilder jsonBuilder = new StringBuilder();

            jsonBuilder.Append("{");
            var properties = obj.GetType().GetProperties();

            foreach (var property in properties)
            {
                var value = property.GetValue(obj);

                if (value != null)
                {
                    jsonBuilder.Append($"\"{property.Name}\":");

                    if (value.GetType().IsClass && value.GetType() != typeof(string))
                    {
                        jsonBuilder.Append(SerializeToJson(value));
                    }
                    else if (value is string)
                    {
                        jsonBuilder.Append($"\"{value}\"");
                    }
                    else
                    {
                        jsonBuilder.Append($"{value}");
                    }

                    jsonBuilder.Append(",");
                }
            }

            if (jsonBuilder[jsonBuilder.Length - 1] == ',')
                jsonBuilder.Length--; 

            jsonBuilder.Append("}");
            return jsonBuilder.ToString();
        }
    }
}