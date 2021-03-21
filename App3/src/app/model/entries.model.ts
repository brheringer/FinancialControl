import { Collection } from '../core/model/collection';
import { EntityReference } from '../core/model/entity-reference';
import { Entry } from './entry.model';

export class Entries extends Collection {
  filterInitialDate: Date;
  filterFinalDate: Date;
  filterExactDate: Date;
  filterLowerValue: number;
  filterHigherSuperior: number;
  filterExactValue: number;
  filterMemo: string;
  filterAccount: EntityReference;
  filterCenter: EntityReference;
  entries: Array<Entry> = new Array<Entry>();
}
