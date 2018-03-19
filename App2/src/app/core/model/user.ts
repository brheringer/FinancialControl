import { DataTransferObject } from './data-transfer-object';

export class User extends DataTransferObject {
    realName: string;
    userName: string;
    newPassword: string;
    banished: boolean;
    banishedReason: string;
    email: string;
}
