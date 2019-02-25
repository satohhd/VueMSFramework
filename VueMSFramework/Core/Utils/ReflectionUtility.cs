using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using VueMSFramework.ViewModels.Common.SpecialParts;

namespace VueMSFramework.Core.Utils
{
    public static class ReflectionUtility
    {
        private static IEnumerable<string> _exclusions = new List<string>();

        //public static string CreateKey(string userName)
        //{
        //    // 共通鍵を用意
        //    var keyString = userName + userName + userName;
        //    // トークン操作用のクラスを用意
        //    var handler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
        //    // 共通鍵なのでSymmetricSecurityKeyクラスを使う
        //    // 引数は鍵のバイト配列
        //    var key = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));
        //    // 署名情報クラスを生成
        //    // 共通鍵を使うのでアルゴリズムはHS256使っとけばいいはず
        //    var credentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(key, "HS256");
        //    // トークンの詳細情報クラス？を生成
        //    var descriptor = new Microsoft.IdentityModel.Tokens.SecurityTokenDescriptor
        //    {
        //        Issuer = userName,
        //        SigningCredentials = credentials,
        //    };
        //    // トークンの生成
        //    //SecurityTokenDescriptor使わずにhandler.CreateJwtSecurityToken("GHKEN", null, null, null, null, null, credentials)でもOK
        //    var token = handler.CreateJwtSecurityToken(descriptor);
        //    // トークンの文字列表現を取得
        //    var tokenString = handler.WriteToken(token);
        //    // eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYmYiOjE0ODc4MjQ3MTQsImV4cCI6MTQ4NzgyODMxNCwiaWF0IjoxNDg3ODI0NzE0LCJpc3MiOiJHSEtFTiJ9.PJ-5KzFq7n2hBiJnoZMli0XajaJPNup0BztIO9QlDFY
        //    return tokenString;
        //}



        /// <summary>
        /// プロパティ情報の取得
        /// </summary>
        /// <param name="type">型</param>
        /// <param name="name">プロパティ名</param>
        /// <returns>プロパティ情報</returns>
        public static PropertyInfo GetPropertyInfo(Type type, string name)
        {
            var property = type.GetProperty(name);

            return property;
        }
        /// <summary>
        /// 先頭の文字を小文字に変換する
        /// </summary>
        /// <param name="convertText">変換する文字列</param>
        /// <returns>変換後の文字列</returns>
        private static string ConvertFirstTextLowerCase(string convertText)
        {
            var returnText = string.Empty;
            for (var i = 0; i < convertText.Length; i++)
            {
                if (i == 0) returnText += convertText[i].ToString().ToLower();
                else returnText += convertText[i];
            }

            return returnText;
        }
        public static void Model2Model<T, T2>(T fromModel, T2 toModel,string exclusions)
        {
            _exclusions = exclusions.Split(',');
            Model2Model(fromModel,toModel);
            _exclusions = new List<string>();

        }

        public static void Model2Model<T, T2>(T fromModel, T2 toModel)
        {
            PropertyInfo[] to = toModel.GetType().GetProperties();

            //モデルからアノテーション情報を取得する
            var dna = new DnaManager();
            var to_ap =  dna.GetDna(toModel.GetType().AssemblyQualifiedName).Fields;
            var fr_ap = dna.GetDna(fromModel.GetType().AssemblyQualifiedName).Fields;


            // プロパティ情報出力をループで回す
            foreach (PropertyInfo info in to)
            {
                if (_exclusions.Contains(info.Name)) continue;

                var property = typeof(T).GetProperty(info.Name);
                if (property != null)
                {
                    try
                    {
                        var val = property.GetValue(fromModel);
                        if (val == null)
                        {
                            info.SetValue(toModel, null);
                        }
                        else
                        {
                            Type to_t = info.PropertyType;
                            Type fr_t = property.PropertyType;
                            //アノテーション属性取得
                            var to_f = (from x in to_ap
                                        where x.Name.Equals(ConvertFirstTextLowerCase(info.Name))
                                        select x).FirstOrDefault();

                            var fr_f = (from x in fr_ap
                                        where x.Name.Equals(ConvertFirstTextLowerCase(property.Name))
                                        select x).FirstOrDefault();


                            if (to_f == null || fr_f == null)
                            {
                                info.SetValue(toModel, val);

                                // DateTime型から⇒string型
                            }
                            else if ((fr_t.Equals(typeof(DateTime)) || fr_t.Equals(typeof(DateTime?))) && to_t.Equals(typeof(string)))
                            {
                                if (val == null)
                                {
                                    info.SetValue(toModel, null);

                                }
                                else
                                {
                                    if (to_f.Type.Equals("date"))
                                    {   //日付の場合
                                        info.SetValue(toModel, ((DateTime)val).ToString("yyyy-MM-dd"));
                                    }
                                    else if (to_f.Type.Equals("time"))
                                    {   //時間の場合
                                        info.SetValue(toModel, ((DateTime)val).ToString("hh:mm:ss"));
                                    }
                                    else if (to_f.Type.Equals("datetime-local") || to_f.Type.Equals("datetime"))
                                    {
                                        //日時の場合
                                        info.SetValue(toModel, ((DateTime)val).ToString("yyyy/MM/dd hh:mm:ss"));
                                    }
                                }



                            }
                            else if ((fr_t.Equals(typeof(DateTime)) || fr_t.Equals(typeof(DateTime?))) && (to_t.Equals(typeof(DateTime?)) || to_t.Equals(typeof(DateTime))))
                            {
                                info.SetValue(toModel, val);
                            }
                            else if ((to_t.Equals(typeof(DateTime?)) || to_t.Equals(typeof(DateTime))) && fr_t.Equals(typeof(string)))
                            {
                                info.SetValue(toModel, DateTime.Parse(val.ToString()));
                            }
                            else if (fr_t.Equals(typeof(File)) && to_t.Equals(typeof(string)))
                            {
                                var f = (File)val;
                                info.SetValue(toModel, (f.FileName + "," + f.FileType + "," + f.Url + "," + f.FileSize));
                            }
                            else if (to_t.Equals(typeof(File)) && fr_t.Equals(typeof(string)))
                            {
                                var f = new File(val.ToString());
                                info.SetValue(toModel, f);
                            }
                            else
                            {
                                info.SetValue(toModel, val);
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(info.Name + ":" + ex.Message);
                        throw ex;
                    }

                }
            }

        }
    }
}
