using System;
using System.ServiceModel.Configuration;

namespace LeandroSoftware.ServicioWeb.EndPoints
{
    public class ConsoleOutputBehaviorExtensionElement : BehaviorExtensionElement
    {
        protected override object CreateBehavior()
        {
            return new ConsoleOutputBehavior();
        }

        public override Type BehaviorType
        {
            get
            {
                return typeof(ConsoleOutputBehavior);
            }
        }
    }
}