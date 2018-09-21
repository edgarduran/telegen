using System;
using System.Linq;
using telegen.Agents.Interfaces;
using telegen.Operations;
using telegen.Results;

/// <summary>
/// Agents exercise features of their domain in response to operations. 
/// </summary>
public abstract class Agent : IAgent
{
    /// <summary>
    /// Identifies the telemetry domain (Network, File, Process, Registry, etc.) accessed by this agent.
    /// </summary>
    /// <remarks>
    /// By default, the agent name is the Domain name with an "Agent" suffix. If we
    /// follow this convention, then we don't have to worry about the Domain property having 
    /// the correct value. It's assigned properly in the base class.
    /// </remarks>
    public string Domain { get; protected set; }

    protected Agent()
    {

        Domain = GetType().Name.Replace("Agent", string.Empty);
    }

    /// <summary>
    /// Ensure that the message is appropriate for this agent.
    /// </summary>
    /// <param name="msg">The unvetted message.</param>
    /// <param name="permittedActions">A list of actions permitted in the calling context.</param>
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

    /// <summary>
    /// Execute the specified action.
    /// </summary>
    /// <returns>The result of the operation.</returns>
    /// <param name="oper">The operation to execute.</param>
    public abstract Result Execute(Operation oper);

}