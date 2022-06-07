using Bogus;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using Bogus.DataSets;
using Vapour.Model;

namespace Vapour.Database
{
    internal class DataSeeder
    {
        private static VapourDatabaseEntities _dataContext;
        private static readonly Random _random = new Random();
        
        public DataSeeder(VapourDatabaseEntities dataContext)
        {
            _dataContext = dataContext;
        }
        
        public void Seed()
        {
            if (_dataContext.Database.Exists())
            {
                if (!_dataContext.Roles.Any())
                {
                    var roles = AddRoles();
                    _dataContext.Roles.AddRange(roles);
                    _dataContext.SaveChanges();
                }

                if (!_dataContext.Users.Any())
                {
                    var users = AddUsers();
                    _dataContext.Users.AddRange(users);

                    var randomGeneratedUsers = AddRandomGeneratedUsers();
                    _dataContext.Users.AddRange(randomGeneratedUsers);

                    _dataContext.SaveChanges();
                }

                if (!_dataContext.Genres.Any())
                {
                    var genres = AddGenres();
                    _dataContext.Genres.AddRange(genres);
                    _dataContext.SaveChanges();
                }

                if (!_dataContext.Games.Any())
                {
                    var games = AddGames();
                    _dataContext.Games.AddRange(games);

                    var randomGeneratedGames = AddRandomGeneratedGames();
                     _dataContext.Games.AddRange(randomGeneratedGames);
                  
                     _dataContext.SaveChanges();
                }

                if (!_dataContext.Comments.Any())
                {
                    var comments = AddComments();
                    _dataContext.Comments.AddRange(comments);
                    _dataContext.SaveChanges();
                }

                if (!_dataContext.Rates.Any())
                {
                    var rates = AddRates();
                    _dataContext.Rates.AddRange(rates);
                    _dataContext.SaveChanges();
                }

                if (!_dataContext.GamesCollections.Any())
                {
                    var gamesCollections = AddGamesCollections();
                    _dataContext.GamesCollections.AddRange(gamesCollections);

                    var (generatedGamesCollections, generatedRates, generatedComments) 
                        = AddRandomGeneratedGamesCollectionCommentsRates();
                    _dataContext.GamesCollections.AddRange(generatedGamesCollections);
                    _dataContext.Rates.AddRange(generatedRates);
                    _dataContext.Comments.AddRange(generatedComments);
                    
                    _dataContext.SaveChanges();
                }

                if (!_dataContext.Follows.Any())
                {
                    var follows = AddFollows();
                    _dataContext.Follows.AddRange(follows);

                    var randomGeneratedFollowers = AddRandomGeneratedFollowers();
                    _dataContext.Follows.AddRange(randomGeneratedFollowers);

                    _dataContext.SaveChanges();
                }
            }
        }
        
        private static IEnumerable<User> AddRandomGeneratedUsers()
        {
            var usersGenerator = new Faker<User>()
                .RuleFor(g => g.Name, f => f.Person.UserName)
                .RuleFor(g => g.Email, f => f.Person.Email)
                .RuleFor(g => g.Password, "AJUJI40TXwnwB84pQXW7xXQizUwx6jSwfPWwbhR/G2OPOjTeCNQu12IHabvFOebKkA==")
                .RuleFor(g => g.WalletBalance, f => decimal.Round(f.Random.Decimal(0, 1000), 2))
                .RuleFor(g => g.RoleId, f => f.Random.Int(1, 2))
                .RuleFor(g => g.Description, f =>
                {
                    var description = f.Lorem.Sentence(_random.Next(10, 50));
                    return description.Length > 255 ? description.Substring(0, 250) + "..." : description;
                });

            var generatedUsers = usersGenerator.Generate(50);
            return generatedUsers;
        }

        private static Tuple<List<GamesCollection>, List<Rate>, List<Comment>> AddRandomGeneratedGamesCollectionCommentsRates()
        {
            var gamesCollections = new List<GamesCollection>();
            var rates = new List<Rate>();
            var comments = new List<Comment>();
            
            for (var i = 5; i < _dataContext.Users.Count(); i++)
            {
                var randomGames = InitializeArrayWithNoDuplicates(10, 1, _dataContext.Games.Count());

                foreach (var gameId in randomGames)
                {
                    var gameCollectionsGenerator = new Faker<GamesCollection>()
                        .RuleFor(g => g.GameId, gameId)
                        .RuleFor(g => g.UserId, i);
                    
                    var generatedGamesCollection = gameCollectionsGenerator.Generate();
                    gamesCollections.Add(generatedGamesCollection);
                    
                    var ratesGenerator = new Faker<Rate>()
                        .RuleFor(g => g.Rate1, f => f.Random.Int(1, 5))
                        .RuleFor(g => g.GameId, gameId)
                        .RuleFor(g => g.UserId, i);
                
                    var generatedRate = ratesGenerator.Generate();
                    rates.Add(generatedRate);
                    
                    var commentsGenerator = new Faker<Comment>()
                        .RuleFor(g => g.Text, f =>
                        {
                            var text = f.Commerce.ProductDescription();
                            return text.Length > 255 ? text.Substring(0, 250) + "..." : text;
                        })
                        .RuleFor(g => g.CreatedAt, f => f.Date.Past())
                        .RuleFor(g => g.GameId, gameId)
                        .RuleFor(g => g.UserId, i);

                    var generatedComment = commentsGenerator.Generate(_random.Next(1, 3));
                    comments.AddRange(generatedComment);
                }
            }

            return Tuple.Create(gamesCollections, rates, comments);;
        }

        private static IEnumerable<int> InitializeArrayWithNoDuplicates(int size, int min, int max)
        {
            var randomNumbers = new HashSet<int>();
            for (var i = 0; i < size; i++)
            {
                randomNumbers.Add(_random.Next(min, max));
            }
            return randomNumbers;
        }

        private static IEnumerable<Follow> AddRandomGeneratedFollowers()
        {
            var followers = new List<Follow>();

            for (var userId = 5; userId < _dataContext.Users.Count(); userId++)
            {
                var randomFollowers = InitializeArrayWithNoDuplicates(10, 1, _dataContext.Users.Count()).ToList();
                randomFollowers.Remove(userId);

                foreach (var followerId in randomFollowers)
                {
                    var followersGenerator = new Faker<Follow>()
                        .RuleFor(g => g.UserId, userId)
                        .RuleFor(g => g.FollowerId, followerId);

                    var follower = followersGenerator.Generate();

                    followers.Add(follower);
                }
            }

            return followers;
        }

        private static IEnumerable<Game> AddRandomGeneratedGames()
        {
            var gamesGenerator = new Faker<Game>()
                .RuleFor(g => g.Title, f =>
                {
                    var title = f.Commerce.Product();
                    var subtitle = f.Lorem.Words(_random.Next(0, 4));
                    return char.ToUpper(title[0]) + title.Substring(1) + " " + string.Join(" ", subtitle);
                })
                .RuleFor(g => g.Description, f =>
                {
                    var description = f.Lorem.Sentence(_random.Next(10, 50));
                    return description.Length > 255 ? description.Substring(0, 250) + "..." : description;
                })
                .RuleFor(g => g.GenreId, f => f.Random.Int(1, 9))
                .RuleFor(g => g.Price, f => decimal.Round(f.Random.Decimal(0, 300), 2))
                .RuleFor(g => g.ReleaseDate, f => f.Date.Between(new DateTime(1990, 1, 1), DateTime.Now));

            var generatedGames = gamesGenerator.Generate(100);
            return generatedGames;
        }

        private static IEnumerable<Role> AddRoles()
        {
            var roles = new List<Role>()
            {
                new Role()
                {
                    Name = "Admin"
                },
                new Role()
                {
                    Name = "User"
                }
            };

            return roles;
        }

        private static IEnumerable<User> AddUsers()
        {
            var users = new List<User>()
            {
                new User()
                {
                    Name = "Patryk",
                    Email = "patryk@test.pl",
                    Password = "AJUJI40TXwnwB84pQXW7xXQizUwx6jSwfPWwbhR/G2OPOjTeCNQu12IHabvFOebKkA==",
                    WalletBalance = 2115,
                    RoleId = 1,
                    Description = "opis"
                },
                new User()
                {
                    Name = "Mariusz",
                    Email = "mariusz@test.pl",
                    Password = "AJUJI40TXwnwB84pQXW7xXQizUwx6jSwfPWwbhR/G2OPOjTeCNQu12IHabvFOebKkA==",
                    WalletBalance = 420,
                    RoleId = 1,
                    Description = "mariano italiano"
                },
                new User()
                {
                    Name = "Arek",
                    Email = "arek@test.pl",
                    Password = "AJUJI40TXwnwB84pQXW7xXQizUwx6jSwfPWwbhR/G2OPOjTeCNQu12IHabvFOebKkA==",
                    WalletBalance = 2137,
                    RoleId = 2,
                    Description = "arek"
                },
                new User()
                {
                    Name = "Dawid",
                    Email = "dawid@test.pl",
                    Password = "AJUJI40TXwnwB84pQXW7xXQizUwx6jSwfPWwbhR/G2OPOjTeCNQu12IHabvFOebKkA==",
                    WalletBalance = 69,
                    RoleId = 2,
                    Description = "opis"
                },
                new User()
                {
                    Name = "test",
                    Email = "test@test.pl",
                    Password = "haslo",
                    WalletBalance = 123456,
                    RoleId = 2,
                    Description = "bez opisu"
                }
            };

            return users;
        }

        private static IEnumerable<Genre> AddGenres()
        {
            var genres = new List<Genre>()
            {
                new Genre()
                {
                    Name = "Action"
                },
                new Genre()
                {
                    Name = "Shooter"
                },
                new Genre()
                {
                    Name = "Sports games"
                },
                new Genre()
                {
                    Name = "Role-playing"
                },
                new Genre()
                {
                    Name = "Fighting"
                },
                new Genre()
                {
                    Name = "Racing"
                },
                new Genre()
                {
                    Name = "Strategy"
                },
                new Genre()
                {
                    Name = "Casual"
                },
                new Genre()
                {
                    Name = "Arcade"
                }
            };

            return genres;
        }

        private static IEnumerable<Game> AddGames()
        {
            var games = new List<Game>()
            {
                new Game()
                {
                    Title = "Doom Eternal",
                    Price = 200.99m,
                    Description = "Become the Slayer in an epic single-player campaign to conquer demons across dimensions and stop the final destruction of humanity.",
                    ReleaseDate = new DateTime(2020, 3, 20),
                    GenreId = 2
                },
                new Game()
                {
                    Title = "Terraria",
                    Price = 35.99m,
                    Description = "Minecraft but in 2D",
                    ReleaseDate = new DateTime(2011, 5, 16),
                    GenreId = 1
                },
                new Game()
                {
                    Title = "Ghost Of Tsushima",
                    Price = 159.49m,
                    Description = "Featuring an open world, the player controls Jin Sakai, a samurai on a quest to protect Tsushima Island during the first Mongol invasion of Japan.",
                    ReleaseDate = new DateTime(2020, 7, 17),
                    GenreId = 1
                },
                new Game()
                {
                    Title = "League Of Legends",
                    Price = 0.0m,
                    Description = "League of Legends is a team-based game with over 140 champions to make epic plays with.",
                    ReleaseDate = new DateTime(2009, 10, 27),
                    GenreId = 7
                },
                new Game()
                {
                    Title = "Minecraft",
                    Price = 120.0m,
                    Description = "Terraria but in 3D",
                    ReleaseDate = new DateTime(2011, 11, 18),
                    GenreId = 8
                },
                new Game()
                {
                    Title = "Elden Ring",
                    Price = 249.00m,
                    Description = "Elden Ring is presented through a third-person perspective, with players freely roaming its interactive open world. Gameplay elements include combat featuring several types of weapons and magic spells, horseback riding, summons, and crafting.",
                    ReleaseDate = new DateTime(2022, 2, 25),
                    GenreId = 4
                },
                new Game()
                {
                    Title = "The Witcher 3 - Wild Hunt",
                    Price = 42.0m,
                    Description = "The Witcher 3: Wild Hunt is an action role-playing game with a third-person perspective. Players control Geralt of Rivia, a monster slayer known as a Witcher",
                    ReleaseDate = new DateTime(2015, 5, 18),
                    GenreId = 4
                },
                new Game()
                {
                    Title = "Call of Duty - Cold War",
                    Price = 78.34m,
                    Description = "Call of Duty: Black Ops Cold War is set during the Cold War in the early 1980s.",
                    ReleaseDate = new DateTime(2020, 11, 13),
                    GenreId = 2
                },
                new Game()
                {
                    Title = "Far Cry 3",
                    Price = 12.87m,
                    Description = "Far Cry 3 is a first-person shooter set on the fictional Rook Islands, a tropical archipelago somewhere in the Pacific, controlled by pirates and mercenaries.",
                    ReleaseDate = new DateTime(2012, 11, 29),
                    GenreId = 2
                },
                new Game()
                {
                    Title = "Hades",
                    Price = 80.0m,
                    Description = "Hades in the ancient Greek religion and myth, is the god of the dead and the king of the underworld, with which his name became synonymous.",
                    ReleaseDate = new DateTime(2020, 9, 17),
                    GenreId = 9
                },
                new Game()
                {
                    Title = "Grand Theft Auto V",
                    Price = 45.45m,
                    Description = "Grand Theft Auto V is an action-adventure game played from either a third-person or first-person perspective.",
                    ReleaseDate = new DateTime(2013, 9, 17),
                    GenreId = 2
                }
            };

            return games;
        }

        private static IEnumerable<Comment> AddComments()
        {
            var comments = new List<Comment>()
            {
                new Comment()
                {
                    Text = "Fajne",
                    CreatedAt = new DateTime(2022, 2, 12, 12, 3, 1),
                    GameId = 1,
                    UserId = 2
                },
                new Comment()
                {
                    Text = "nie fajne",
                    CreatedAt = new DateTime(2022, 3, 13, 7, 31, 41),
                    GameId = 2,
                    UserId = 2
                },
                new Comment()
                {
                    Text = "mi sie podoba",
                    CreatedAt = new DateTime(2022, 3, 13, 7, 31, 17),
                    GameId = 1,
                    UserId = 3
                },
                new Comment()
                {
                    Text = "2/10",
                    CreatedAt = new DateTime(2022, 1, 21, 8, 12, 13),
                    GameId = 3,
                    UserId = 3
                },
                new Comment()
                {
                    Text = "gupie",
                    CreatedAt = new DateTime(2022, 1, 21, 8, 12, 13),
                    GameId = 1,
                    UserId = 4
                },
            };

            return comments;
        }

        private static IEnumerable<Rate> AddRates()
        {
            var rates = new List<Rate>()
            {
                new Rate()
                {
                    Rate1 = 5,
                    UserId = 1,
                    GameId = 1
                },
                new Rate()
                {
                    Rate1 = 4,
                    UserId = 1,
                    GameId = 2
                },
                new Rate()
                {
                    Rate1 = 3,
                    UserId = 1,
                    GameId = 3
                },
                new Rate()
                {
                    Rate1 = 3,
                    UserId = 2,
                    GameId = 1
                },
                new Rate()
                {
                    Rate1 = 2,
                    UserId = 3,
                    GameId = 1
                },
                new Rate()
                {
                    Rate1 = 1,
                    UserId = 4,
                    GameId = 1
                },
                new Rate()
                {
                    Rate1 = 3,
                    UserId = 2,
                    GameId = 2
                },
                new Rate()
                {
                    Rate1 = 6,
                    UserId = 2,
                    GameId = 3
                },
            };

            return rates;
        }

        private static IEnumerable<GamesCollection> AddGamesCollections()
        {
            var gamesCollections = new List<GamesCollection>()
            {
                new GamesCollection()
                {
                    UserId = 1,
                    GameId = 1
                },
                new GamesCollection()
                {
                    UserId = 1,
                    GameId = 2
                },
                new GamesCollection()
                {
                    UserId = 1,
                    GameId = 3
                },
                new GamesCollection()
                {
                    UserId = 2,
                    GameId = 1
                },
                new GamesCollection()
                {
                    UserId = 3,
                    GameId = 1
                },
                new GamesCollection()
                {
                    UserId = 3,
                    GameId = 2
                },
                new GamesCollection()
                {
                    UserId = 4,
                    GameId = 4
                },
                new GamesCollection()
                {
                    UserId = 4,
                    GameId = 2
                },
                new GamesCollection()
                {
                    UserId = 3,
                    GameId = 3
                }
            };

            return gamesCollections;
        }

        private static IEnumerable<Follow> AddFollows()
        {
            var follows = new List<Follow>()
            {
                new Follow()
                {
                    UserId = 1,
                    FollowerId = 2
                },
                new Follow()
                {
                    UserId = 1,
                    FollowerId = 4
                },
                new Follow()
                {
                    UserId = 2,
                    FollowerId = 1
                },
                new Follow()
                {
                    UserId = 2,
                    FollowerId = 4
                },
                new Follow()
                {
                    UserId = 3,
                    FollowerId = 1
                },
                new Follow()
                {
                    UserId = 4,
                    FollowerId = 3
                },
            };

            return follows;
        }
    }
}
