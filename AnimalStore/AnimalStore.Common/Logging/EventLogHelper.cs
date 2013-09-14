using System.Diagnostics;
using System.Security;

namespace AnimalStore.Common.Logging
{
    public static class EventLogHelper
    {
        public static void InitialiseEventLog()
       {
           const string eventlogName = "AnimalStore.Events";

           try
           {
               if (!EventLog.SourceExists(eventlogName))
               {
                   EventLog.CreateEventSource(eventlogName, eventlogName);

                   var eventLog = new EventLog { Source = eventlogName, Log = eventlogName };
                   eventLog.Source = eventlogName;
                   eventLog.WriteEntry("The " + eventlogName + " was successfully created.",
                                       EventLogEntryType.Information);
               }
           }
           catch(SecurityException)
           {
               string user = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
               string eventLogRegistryKey ="HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Services\\EventLog\\Security";
               string message ="In order to use logging, the Event logs must be accessible. You must grant " + user + "read permissions on the registry key " + eventLogRegistryKey;
               throw new SecurityException(message);
           }
       }
    }
}
