using System.Diagnostics;

namespace AnimalStore.Common.Logging
{
    public static class EventLogHelper
    {
        public static void InitialiseEventLog()
       {
           const string eventlogName = "AnimalStore.Events";

           if (!EventLog.SourceExists(eventlogName))
           {
               EventLog.CreateEventSource(eventlogName, eventlogName);

               var eventLog = new EventLog { Source = eventlogName, Log = eventlogName };
               eventLog.Source = eventlogName;
               eventLog.WriteEntry("The " + eventlogName + " was successfully created.", 
                                   EventLogEntryType.Information);
           }
       }
    }
}
