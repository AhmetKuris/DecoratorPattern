using System;

namespace DecoratorPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            IUserService userService = new UserService();
            SayHelloDecorator sayHelloDecorator = new SayHelloDecorator(userService);

            DummyLogger dummyLogger = new DummyLogger();
            LogggingDecorator logggingDecorator = new LogggingDecorator(sayHelloDecorator, dummyLogger);

            logggingDecorator.Login("MyUsername123","MyPassword123");
        }

        //Base Interface
        interface IUserService
        {
            void Login(string username, string password);
        }

        //Concrete Implementation
        class UserService : IUserService
        {
            public void Login(string username, string password)
            {
                Console.WriteLine("User Logged In.");
            }
        }

        //Base Decorator
        class UserDecorator : IUserService
        {
            private IUserService _userService;

            public UserDecorator(IUserService userService)
            {
                _userService = userService;
            }

            public virtual void Login(string username, string password)
            {
                _userService.Login(username, password);
            }
        }

        //Concerete Decorators
        class LogggingDecorator : UserDecorator
        {
            private DummyLogger _dummyLogger;

            public LogggingDecorator(IUserService userService, DummyLogger dummyLogger) : base(userService)
            {
                _dummyLogger = dummyLogger;
            }

            public override void Login(string username, string password)
            {
                _dummyLogger.Log($"Start {nameof(Login)} with username: " 
                                 + username + " password: " + password);

                base.Login(username, password);

                _dummyLogger.Log($"End {nameof(Login)}");

            }
        }

        class SayHelloDecorator : UserDecorator
        {
            public SayHelloDecorator(IUserService userService) : base(userService)
            {
            }

            public override void Login(string username, string password)
            {
                Console.WriteLine("I just want to say 'Hi!'");

                base.Login(username, password);

                Console.WriteLine("I just want to say 'Goodbye!'");
            }
        }


        //Dummy Logger Class
        class DummyLogger
        {
            public void Log(params string[] Parameters)
            {
                foreach (var item in Parameters)
                {
                    Console.WriteLine("Dummy Log : " + item);
                }
            }
        }
    }
}
