import { DataTransferObject } from '../../core/model/data-transfer-object';
import { EntityReference } from '../../core/model/entity-reference';
import { Entry } from 'src/app/model/entry.model';

export class Dashboard extends DataTransferObject {
  filterInitialDate: Date;
  filterFinalDate: Date;
  filterAccount: EntityReference = null;
  filterResultCenter: EntityReference = null;
  filterMemo: string = null;
  filterMinValue: number = 0;
  filterMaxValue: number = 0;
  filterAccountLevel: number = 0;
  entries: Entry[] = [];
  periodicity: string = null;
}
