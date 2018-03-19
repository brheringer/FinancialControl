/* SystemJS module definition */
declare var module: NodeModule;
interface NodeModule {
  id: string;
}

//https://stackoverflow.com/questions/42110817/load-config-json-file-in-angular-2
declare module "*.json" {
  const value: any;
  export default value;
}
