using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VueMSFramework.Models;
using VueMSFramework.Models.Auth;
using VueMSFramework.Models.Keicho;

namespace VueMSFramework.Data
{
    public class SeedData
    {
        public static void Erase(ApplicationDbContext context, string tableName)
        {
            try
            {


            }
            catch
            {
                //return;

            }

        }

 
        public static async Task MigrateAsync(ApplicationDbContext context)
        {
            try
            {
                await context.Database.MigrateAsync();
            }
            catch
            {
                //return;

            }

        }
        public static void Erase(ApplicationDbContext context)
        {
            try
            {
                context.Database.EnsureDeleted();
            }
            catch
            {
                //return;

            }

        }
        public static  void Make(ApplicationDbContext context)
        {
            try
            {
                context.Database.EnsureCreated();

                context.SaveChanges();

                //データが存在しない場合
                if (!context.Users.Any())
                {
                    new List<Account>
                    {
                        new Account
                        {
                            AccountId = Guid.NewGuid().ToString(),
                            UserName = "jimukyoku@tobicrun.jp",
                            Registed  = DateTime.Now,
                            Updated = DateTime.Now,
                            Version = 1,

                        },
                       new Account
                        {
                            AccountId = Guid.NewGuid().ToString(),
                            UserName = "jimukyoku",
                            Registed  = DateTime.Now,
                            Updated = DateTime.Now,
                            Version = 1,
                       },
                       new Account
                        {
                            AccountId = Guid.NewGuid().ToString(),
                            UserName = "satohhd",
                            Registed  = DateTime.Now,
                            Updated = DateTime.Now,
                            Version = 1,
                        },
                      new Account
                        {
                            AccountId = Guid.NewGuid().ToString(),
                            UserName = "tomo",
                            Registed  = DateTime.Now,
                            Updated = DateTime.Now,
                            Version = 1,
                        },

                    }.ForEach(u => context.Accounts.Add(u));
                    // 変更をデータベースに反映
                    int recordsAffected = context.SaveChanges();

                }

                //データが存在しない場合
                if (!context.Options.Any())
                {
                    new List<Option>
                    {
                        new Option
                        {
                            OptionId = Guid.NewGuid().ToString(),
                            Value = "shukin",
                            Text = "出勤",
                            Field = "Knmkbn",
                            OrderBy = 1,
                        },
                        new Option
                        {
                            OptionId = Guid.NewGuid().ToString(),
                            Value = "kyuka",
                            Text = "休暇",
                            Field = "Knmkbn",
                            OrderBy = 2,
                        },

                        new Option
                        {
                            OptionId = Guid.NewGuid().ToString(),
                            Value = "111",
                            Text = "ｶﾃｺﾞﾘ１",
                            Field = "Category",
                            OrderBy = 3,
                       },
                        new Option
                        {
                            OptionId = Guid.NewGuid().ToString(),
                            Value = "2222",
                            Text = "ｶﾃｺﾞﾘ２",
                            Field = "Category",
                            OrderBy = 4,
                        },


                        new Option
                        {
                            OptionId = Guid.NewGuid().ToString(),
                            Value = "TAKE",
                            Text = "もらいました",
                            Field = "KeichoClass",
                            OrderBy = 5,
                        },
                        new Option
                        {
                            OptionId = Guid.NewGuid().ToString(),
                            Value = "GIVE",
                            Text = "あげました",
                            Field = "KeichoClass",
                            OrderBy = 6,
                        },
                        new Option
                        {

                            OptionId = Guid.NewGuid().ToString(),
                            Value = "NGKIWI",
                            Text = "入学祝",
                            Field = "KeichoTypeId",
                            OrderBy = 7,
                        },
                        new Option
                        {
                            OptionId = Guid.NewGuid().ToString(),
                            Value = "NENIWI",
                            Text = "入園祝",
                            Field = "KeichoTypeId",
                            OrderBy = 8,
                        },
                        new Option
                        {
                            OptionId = Guid.NewGuid().ToString(),
                            Value = "SGKIWI",
                            Text = "進学祝",
                            Field = "KeichoTypeId",
                            OrderBy =9,
                        },

                        new Option
                        {
                            OptionId = Guid.NewGuid().ToString(),
                            Value = "SSNIWI",
                            Text = "出産祝",
                            Field = "KeichoTypeId",
                            OrderBy = 10,
                        },


                        new Option
                        {
                            OptionId = Guid.NewGuid().ToString(),
                            Value = "KKIWI",
                            Text = "結婚祝",
                            Field = "KeichoTypeId",
                            OrderBy = 1,
                        },

                        new Option
                        {
                            OptionId = Guid.NewGuid().ToString(),
                            Value = "SSKIWI",
                            Text = "就職祝",
                            Field = "KeichoTypeId",
                            OrderBy = 1,
                       },

                        new Option
                        {
                            OptionId = Guid.NewGuid().ToString(),
                            Value = "OTSDM",
                            Text = "お年玉",
                            Field = "KeichoTypeId",
                            OrderBy = 1,
                        },

                        new Option
                        {
                            OptionId = Guid.NewGuid().ToString(),
                            Value = "OKDKI",
                            Text = "お小遣い",
                            Field = "KeichoTypeId",
                            OrderBy = 1,
                        },
                        new Option
                        {
                            OptionId = Guid.NewGuid().ToString(),
                            Value = "SKIWI",
                            Text = "節句祝",
                            Field = "KeichoTypeId",
                            OrderBy = 1,
                        },
                        new Option
                        {
                            OptionId = Guid.NewGuid().ToString(),
                            Value = "GRZN",
                            Text = "御霊前",
                            Field = "KeichoTypeId",
                            OrderBy = 1,
                        },
                        new Option
                        {
                            OptionId = Guid.NewGuid().ToString(),
                            Value = "GFTZN",
                            Text = "御仏前",
                            Field = "KeichoTypeId",
                            OrderBy = 1,
                        },
                        new Option
                        {
                            OptionId = Guid.NewGuid().ToString(),
                            Value = "MISC",
                            Text = "その他任意入力",
                            Field = "KeichoTypeId",
                            OrderBy = 1,
                        },

                    }.ForEach(u => context.Options.Add(u));
                    // 変更をデータベースに反映
                    int recordsAffected = context.SaveChanges();

                }


                //データが存在しない場合
                if (!context.Categories.Any())
                {
                    new List<Category>
                    {
                        new Category
                        {
                            CategoryId = "1111",
                            CategoryName = "マーク式",
                        },
                        new Category
                        {
                            CategoryId = "222",
                            CategoryName = "記述式",
                        }

                    }.ForEach(u => context.Categories.Add(u));
                    // 変更をデータベースに反映
                    int recordsAffected = context.SaveChanges();

                }

                //データが存在しない場合
                if (!context.KeichoTypes.Any())
                {
                    new List<KeichoType>
                    {


                        new KeichoType
                        {
                            KeichoTypeId = "NGKIWI",
                            KeichoTypeName = "入学祝",
                            OrderBy = 1,
                            IsHidden = false,
                            Registed  = DateTime.Now,
                            Updated = DateTime.Now,
                            Version = 1,
                        },
                        new KeichoType{
                            KeichoTypeId = "NENIWI",
                            KeichoTypeName = "入園祝",
                            OrderBy = 2,
                            IsHidden = false,
                            Registed  = DateTime.Now,
                            Updated = DateTime.Now,
                            Version = 1,
                        },
                        new KeichoType{
                            KeichoTypeId = "SGKIWI",
                            KeichoTypeName = "進学祝",
                            OrderBy =3,
                            IsHidden = false,
                            Registed  = DateTime.Now,
                            Updated = DateTime.Now,
                            Version = 1,
                        },
                        new KeichoType{
                            KeichoTypeId = "SSNIWI",
                            KeichoTypeName = "出産祝",
                            OrderBy =4,
                            IsHidden = false,
                            Registed  = DateTime.Now,
                            Updated = DateTime.Now,
                            Version = 1,
                        },
                        new KeichoType{
                            KeichoTypeId = "KKIWI",
                            KeichoTypeName = "結婚祝",
                            OrderBy =5,
                            IsHidden = false,
                            Registed  = DateTime.Now,
                            Updated = DateTime.Now,
                            Version = 1,
                        },
                         new KeichoType{
                            KeichoTypeId = "SSKIWI",
                            KeichoTypeName = "就職祝",
                            OrderBy =6,
                            IsHidden = false,
                            Registed  = DateTime.Now,
                            Updated = DateTime.Now,
                            Version = 1,
                        },
                        new KeichoType{
                            KeichoTypeId = "OTSDM",
                            KeichoTypeName = "お年玉",
                            OrderBy =7,
                            IsHidden = false,
                            Registed  = DateTime.Now,
                            Updated = DateTime.Now,
                            Version = 1,
                        },
                        new KeichoType{
                            KeichoTypeId = "OKDKI",
                            KeichoTypeName = "お小遣い",
                            OrderBy =8,
                            IsHidden = false,
                            Registed  = DateTime.Now,
                            Updated = DateTime.Now,
                            Version = 1,
                        },
                        new KeichoType{
                            KeichoTypeId = "SKIWI",
                            KeichoTypeName = "節句祝",
                            OrderBy =9,
                            IsHidden = false,
                            Registed  = DateTime.Now,
                            Updated = DateTime.Now,
                            Version = 1,
                        },
                        new KeichoType{
                            KeichoTypeId = "GRZN",
                            KeichoTypeName = "御霊前",
                            OrderBy =10,
                            IsHidden = false,
                            Registed  = DateTime.Now,
                            Updated = DateTime.Now,
                            Version = 1,
                        },
                         new KeichoType{
                            KeichoTypeId = "GFTZN",
                            KeichoTypeName = "御仏前",
                            OrderBy =11,
                            IsHidden = false,
                            Registed  = DateTime.Now,
                            Updated = DateTime.Now,
                            Version = 1,
                        },
                         new KeichoType{
                            KeichoTypeId = "MISC",
                            KeichoTypeName = "その他任意入力",
                            OrderBy =12,
                            IsHidden = false,
                            Registed  = DateTime.Now,
                            Updated = DateTime.Now,
                            Version = 1,
                        }
                     }.ForEach(u => context.KeichoTypes.Add(u));
                    // 変更をデータベースに反映
                    int recordsAffected = context.SaveChanges();

                }


            }
            catch
            {
                return;

            }
        }
            public static void Initialize(ApplicationDbContext context)
        {
            try
            {
                context.Database.EnsureDeleted();

            }
            catch
            {
                //return;

            }
            try
            {
                context.Database.EnsureCreated();

            }
            catch
            {
                //return;

            }


            // 変更をデータベースに反映
            int recordsAffected = context.SaveChanges();
        }
    }

}
