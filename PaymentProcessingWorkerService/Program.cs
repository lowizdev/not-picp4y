using Common.Interfaces;
using PaymentProcessingWorkerService;
using PaymentProcessingWorkerService.Facades;
using PaymentProcessingWorkerService.Repositories;
using PaymentProcessingWorkerService.Repositories.UnityOfWork;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

builder.Services.AddScoped<IProcessPaymentFacade, ProcessPaymentFacade>();
builder.Services.AddTransient<IQueryPaymentRepository, SQLitePaymentRepository>();
builder.Services.AddTransient<IWalletFullRepository, SQLiteWalletRepository>();
builder.Services.AddTransient<IQueryUserRepository, SQLiteUserRepository>();
builder.Services.AddScoped<DbSession>();
builder.Services.AddTransient<IUnityOfWork, UnityOfWork>();

var host = builder.Build();
host.Run();
