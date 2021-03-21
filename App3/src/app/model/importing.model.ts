import { DataTransferObject } from '../core/model/data-transfer-object';
import { EntityStatus } from './entityStatus.model';

export class Importing extends DataTransferObject {
  processed: boolean = false;
  success: boolean = false;
  timeStamp: Date;
  quantityOfImportedEntries: number;
  errorsMessages: Array<string> = new Array<string>();
}
