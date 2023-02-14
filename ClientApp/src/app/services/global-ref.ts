export abstract class GlobalRef {
    abstract get nativeGlobal(): IAppGlobalRef;
}

export class BrowserGlobalRef extends GlobalRef {
    get nativeGlobal(): IAppGlobalRef { return <any>window as IAppGlobalRef; }
}
