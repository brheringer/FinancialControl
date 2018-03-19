import { EntityReference } from '../core/model/entity-reference';

export class Detail {
  date: Date;
  value: number;
  memo: string;
  originalEntry: EntityReference;
}
