using System;

public class EventManager : Singelton<EventManager>
{
    public Action<string> OnMessageSent;
}