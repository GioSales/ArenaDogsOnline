using System;
using System.Reflection;
using UnityEngine;
using Unity.Netcode;

namespace SmartConsole
{
    public abstract class NetworkCmndBehaviour : NetworkBehaviour
    {
        protected virtual void Start()
        {
            GetCommandMethods();
        }

        private void GetCommandMethods()
        {
            var methods = GetType().GetMethods(
                    BindingFlags.Public | 
                    BindingFlags.NonPublic | 
                    BindingFlags.Static | 
                    BindingFlags.Instance);
            
            for (int i = 0; i < methods.Length; i++)
            {
                CommandAttribute commandAttribute = 
                    Attribute.GetCustomAttribute(methods[i], typeof(CommandAttribute)) as CommandAttribute;
                
                if (commandAttribute != null)
                {
                    var command = new Command(this, methods[i]);
                    
                    if (!Command.List.Exists(cmd => cmd.MethodInfo.Name == methods[i].Name))
                    {
                        Command.List.Add(command);
                    }
                }
            }
        }
    }
}
