export class EntityStatus {
  error: boolean = false;
  msg: string = '';

  setError(msg: string): void {
    this.msg = msg;
    this.error = true;
  }

  setNonError(msg: string): void {
    this.msg = msg;
    this.error = false;
  }

}
