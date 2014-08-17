using AnimalStore.Common.Constants;
using System.Diagnostics;
using System.Security;
using AnimalStore.Common.Helpers;

namespace AnimalStore.Common.Logging
{
  public static class EventLogHelper
  {
    public static void InitialiseEventLog()
    {
      if (Environment.IsNotDevelopment())
        return;     // TODO: In here, we will handle some Azure logging

      var EVENT_LOG_NAME = EventLogConstants.EVENT_LOG_NAME;

      try
      {
        if (EventLog.SourceExists(EVENT_LOG_NAME)) return;

        SetUpWindowsEventLog(EVENT_LOG_NAME);
      }
      catch (SecurityException)
      {
        HandleSecurityException();
      }
    }

    private static void HandleSecurityException()
    {
      var windowsIdentity = System.Security.Principal.WindowsIdentity.GetCurrent();
      if (windowsIdentity == null) return;

      string user = windowsIdentity.Name;

      const string eventLogRegistryKey = "HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Services\\EventLog\\Security";

      string message = "In order to use logging, the Event logs must be accessible. You must grant " + user +
                       " read permissions on the registry key " + eventLogRegistryKey;

      throw new SecurityException(message);
    }

    private static void SetUpWindowsEventLog(string EVENT_LOG_NAME)
    {
      EventLog.CreateEventSource(EVENT_LOG_NAME, EVENT_LOG_NAME);

      var eventLog = new EventLog {Source = EVENT_LOG_NAME, Log = EVENT_LOG_NAME};

      eventLog.Source = EVENT_LOG_NAME;

      eventLog.WriteEntry(
        EVENT_LOG_NAME + " successfully created.", EventLogEntryType.Information);
    }
  }
}
