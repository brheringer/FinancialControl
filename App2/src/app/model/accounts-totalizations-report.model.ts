import { DataTransferObject } from '../core/model/data-transfer-object';
import { EntityReference } from '../core/model/entity-reference';
import { AccountTotalization } from './account-totalization.model';

export class AccountsTotalizationsReport extends DataTransferObject {
  filterInitialDate: Date;
  filterFinalDate: Date;
  accountsTotalizations: Array<AccountTotalization> = new Array<AccountTotalization>();
}
