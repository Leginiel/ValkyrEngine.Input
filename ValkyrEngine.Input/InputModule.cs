using Autofac;
using System.Diagnostics.CodeAnalysis;

namespace ValkyrEngine.Input
{
  /// <summary>
  /// A class that is used to setup autofac.
  /// </summary>
  [ExcludeFromCodeCoverage]
  public class InputModule : Module
  {
    protected override void Load(ContainerBuilder builder)
    {
      builder.RegisterType<InputSystem>()
             .As<IInputSystem>()
             .SingleInstance();
    }
  }
}
