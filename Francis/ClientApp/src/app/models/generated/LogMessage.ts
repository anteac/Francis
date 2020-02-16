//     This code was generated by a Reinforced.Typings tool. 
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.

import { LogEventLevel } from './LogEventLevel';
import { RuntimeError } from './RuntimeError';

export class LogMessage
{
	public message?: string;
	public level?: LogEventLevel;
	public timestamp?: Date;
	public properties?: { [key:string]: string };
	public exception?: RuntimeError;
}