import { Actor } from "./actor";
import { Rating } from "./rating";

export interface Movie {
    id: number;
    title: string;
    coverUrl: string;
    releaseDate: Date;
    description: string;
    averageRating: number;
    ratings: Rating[];
    isMovie:boolean;
    cast: Actor[];
  }