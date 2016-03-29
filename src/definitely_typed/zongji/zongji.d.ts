// Type definitions for zongji
// Project: https://github.com/nevill/zongji
// Definitions by: Makis Maropoulos <https://github.com/kataras>
// Definitions: https://github.com/borisyankov/DefinitelyTyped
//NOT YET COMPLETED.
///<reference path='./../mysql/mysql.d.ts' />
//or declare function ZongJi.... doesnt worked either.
declare module "zongji" {

	/**
	 * Supported Events:
	 * Event Name | Description
	 * 
	 * unknown		Catch any other events
	 * query		Insert/Update/Delete Query
	 * rotate		New Binlog file (not required to be included to rotate to new files)
	 * format		Format Description
	 * xid			Transaction ID
	 * tablemap		Before any row event (must be included for any other row events)
	 * writerows	Rows inserted
	 * updaterows	Rows changed
	 * deleterows	Rows deleted
	 */
	
	/**
	 * Event Methods. Neither method requires any arguments.
	 * Name		   | 		Description
	 * 
	 * dump					Log a description of the event to the console
	 * getEventName			Return the name of the event
	 */

	import * as Mysql from 'mysql';

    class ZongJi {

		/**
	     * Inits the ZongJi  from the connection url or the real connection object.
	     * @param {string | Mysql.IConnection} connection the connection url or the real connection object.
	     */
		constructor(connection: string | Mysql.IConnection | Mysql.IConnectionConfig);
		 
		/**
		 * Start receiving replication events
		 */
		start(options?: Options): void;
		
		/**
		 * Disconnect from MySQL server, stop receiving events
		 */
		stop(): void
		
		/**
		 * Change options after start()
		 */
		set(options: Options): void;
		
		/**
		 * Add a listener to the binlog or error event. Each handler function accepts one argument.
		 */
		on(eventName: string, handler: (evt: any) => void): void;

	}

	class Options {
		
		/**
		 * Unique number (1 - 232) to identify this replication slave instance.
		 * Must be specified if running more than one instance of ZongJi. 
		 * Must be used in start() method for effect.
		 * Default: 1
		 */
		serverId: number;
		
		/**
		 * Pass true to only emit binlog events that occur after ZongJi's instantiation. 
		 * Must be used in start() method for effect.
		 * Default: false
		 */
		startAtEnd: boolean 
		
		/**
		 * Array of event names to include
         * Example: ['writerows', 'updaterows', 'deleterows']
		 */
		includeEvets: string[];
		
		/**
		 * Array of event names to exclude
		 * Example: ['rotate', 'tablemap']
		 */
		excludeEvents: string[];
		
		/**
		 * Object describing which databases and tables to include (Only for row events).
		 *  Use database names as the key and pass an array of table names or true (for the entire database).
		 * Example: { 'my_database': ['allow_table', 'another_table'], 'another_db': true }
		 */
		includeSchema: any;
		
		/**
		 * Object describing which databases and tables to exclude (Same format as includeSchema)
		 * Example: { 'other_db': ['disallowed_table'], 'ex_db': true }
		 */
		excludeSchema: any;
	}
}