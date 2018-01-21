import { PhotoComponent } from "../../shared/photo/photo.component";

export interface IPhotoService {
    GetAllPhotos(subFolder: string) : Promise<string[]>
}