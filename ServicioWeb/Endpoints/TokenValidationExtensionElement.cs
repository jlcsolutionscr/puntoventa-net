using System;
using System.ServiceModel.Configuration;

namespace LeandroSoftware.ServicioWeb.EndPoints
{
    public class TokenValidationExtensionElement : BehaviorExtensionElement
    {
        protected override object CreateBehavior()
        {
            return new TokenValidationBehavior();
        }

        public override Type BehaviorType
        {
            get
            {
                return typeof(TokenValidationBehavior);
            }
        }
    }
}