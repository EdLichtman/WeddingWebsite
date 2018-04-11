import { IAlbum } from 'angular2-lightbox';
export class Photo implements IAlbum{
    constructor(src: string) {
        this.src = src
    }
    public src: string;
    public caption: string;
    public thumb: string;
    public isSideways: Function = () : boolean => {
        return this.src.includes("horizontal")
    }
} 