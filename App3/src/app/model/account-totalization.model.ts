import { EntityReference } from '../core/model/entity-reference';
import { Detail } from './detail.model';

export class AccountTotalization {
  accountStructure: string;
  accountDescription: string;
  centerStructure: string;
  centerDescription: string;
  total: number;
  accountLevel: number;
  isDetail: boolean;
  analyticalDetails: Array<Detail> = new Array<Detail>();
}
