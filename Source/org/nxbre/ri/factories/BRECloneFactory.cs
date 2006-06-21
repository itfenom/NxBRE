namespace org.nxbre.ri.factories
{
	using System;
	
	using net.ideaity.util.events;

	using org.nxbre;
	using org.nxbre.rule;
	using org.nxbre.ri.drivers;
	
	/// <summary>This factory allows an easy creation of a BRE objects from a unique Clone,<br/>
	/// which is very convenient for a multi-threaded environment where each thread will
	/// use a different clone.</summary>
	/// <author>David Dossot</author>
	/// <version>1.8.2</version>
	public sealed class BRECloneFactory {
		private IRulesDriver rulesDriver = null;
		private BREFactory bref = null;
		private IFlowEngine bre = null;
		
		public BRECloneFactory(IRulesDriver rulesDriver):this(rulesDriver, null, null, null) {}
		
		public BRECloneFactory(IRulesDriver rulesDriver,
		                       DispatchException exceptionHandler):this(rulesDriver, exceptionHandler, null, null) {}
		
		public BRECloneFactory(IRulesDriver rulesDriver,
	                         DispatchException exceptionHandler,
	                  			 DispatchLog logHandler):this(rulesDriver, exceptionHandler, logHandler, null) {}
		
		public BRECloneFactory(IRulesDriver rulesDriver,
	                         DispatchException exceptionHandler,
	                         DispatchLog logHandler,
	                         DispatchRuleResult resultHandler)
		{
			if (rulesDriver == null)
				throw new BREException("A non-null IRulesDriver must be passed to BRECloneFactory");

			this.rulesDriver = rulesDriver;
			
			if (bref == null)
				bref = new BREFactory(exceptionHandler, logHandler, resultHandler);
		}
				
		public IFlowEngine NewBRE() {
			if (bref == null)
				throw new BREException("BRECloneFactory is not correctly initialized.");

			if (bre == null)
				bre = bref.NewBRE(rulesDriver);
			
			if (bre == null)
				throw new BREException("BRECloneFactory could not instantiate an valid IBRE implementation.");
			
			return (IFlowEngine)bre.Clone();
		}

	}
	
}
