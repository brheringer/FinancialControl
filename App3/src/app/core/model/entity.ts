import { DataTransferObject } from './data-transfer-object';

export class Entity extends DataTransferObject {
  autoId: number;
  presentation: string;
  version: number;
  creationDateTime: Date;
  creationUser: string;
  lastUpdateDateTime: Date;
  lastUpdateUser: string;
  user: string;

  public isPersistent(): boolean
  {
    return this.autoId > 0;
  }
}
