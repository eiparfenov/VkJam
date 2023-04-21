using System.Linq;
using System.Reflection;
using Signals;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class SignalsInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            foreach (var type in Assembly.GetExecutingAssembly().GetTypes().Where(x => typeof(ISignal).IsAssignableFrom(x) && !x.IsAbstract))
            {
                Container.DeclareSignal(type).OptionalSubscriber();
            }

            Container.DeclareSignalWithInterfaces<TempInventoryOpenSignal>().OptionalSubscriber();
            //Container.BindSignal<AsteroidAddToMainSignal>()
            //    .ToMethod(x => Debug.Log($"Gained {x.Energy} energy."));
        }
    }
}