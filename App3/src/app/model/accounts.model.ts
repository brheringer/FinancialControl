import { Collection } from '../core/model/collection';
import { Account } from './account.model';

export class Accounts extends Collection {
  filterStructure: string;
  filterDescription: string;
  accounts: Array<Account> = new Array<Account>();
}
