using System.Text.Json.Serialization;
using Dunet;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateSlimBuilder(args);


var app = builder.Build();

var du = app.MapGroup("/du");

du.MapGet("/", () =>
{
    return new Req(new Shape.Circle(20));
});

du.MapPost("/", ([FromBody] Req req) => {
    return req.Param;
});

app.Run();

[Union]
[JsonDerivedType(typeof(Circle), typeDiscriminator: nameof(Circle))]
[JsonDerivedType(typeof(Rectangle), typeDiscriminator: nameof(Rectangle))]
[JsonDerivedType(typeof(Triangle), typeDiscriminator: nameof(Triangle))]
partial record Shape
{
    partial record Circle(double Radius);
    partial record Rectangle(double Length, double Width);
    partial record Triangle(double Base, double Height);
}

partial record Req(Shape Param);
