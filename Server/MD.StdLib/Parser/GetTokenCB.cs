namespace MD.StdLib.Parser {
	// @breif Callback Template for retreiving Tokens from external sources
	// @return new data as Token, if available; null if external source has no data available
	public delegate Token? GetTokenCB();
}

