using System;
using System.Linq;
using telegen.Agents.Interfaces;
using telegen.Operations;
using telegen.Results;

public abstract class Agent : IAgent
{
    public virtual string Domain { get; protected set; }

    public Agent()
    {
        Domain = GetType().Name.Replace("Agent", string.Empty);
    }

    protected void Guard(Operation msg, params string[] permittedActions)
    {
        if (msg.Domain != Domain)
        {
            throw new Exception($"{GetType().Name} received a message for a different domain. Expected {Domain}; received {msg.Domain}.");
        }

        if (permittedActions.Contains(msg.Action)) return;

        if (permittedActions.Count() == 1)
        {
            throw new Exception($"{GetType().Name} received an unexpected message. Expected {Domain}.{permittedActions.First()}; received {msg.Domain}.{msg.Action}.");
        }
        else
        {
            var actions = string.Join(", ", permittedActions);
            throw new Exception($"{GetType().Name} received an unexpected action. Expected one of {actions}; received {msg.Action}.");
        }
    }

    public abstract Result Execute(Operation oper);

}