using System;
namespace AzureFuncLib
{
    /// <summary>
    /// Contains constants and enums for consistent structured logging
    /// </summary>
    public static class LogUtils
    {
        // Template for consisted structured logging accross multiple functions, each field is described below: 
        // EntityType: Business Entity Type being processed: e.g. Order, Shipment, etc.
        // EntityId: Id of the Business Entity being processed: e.g. Order Number, Shipment Id, etc. 
        // Status: Status of the Log Event, e.g. Succeeded, Failed, Discarded.
        // Description: A detailed description of the log event. 
        public const string Template = "{EntityType}, {EntityId}, {Description}";

    }
}
