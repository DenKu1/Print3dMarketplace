export interface CreatePrintRequestModel {
  templateMaterialId: string;
  colorId: string;

  layerHeight: number;
  infill: number;

  printAreaLength: number;
  printAreaWidth: number;
  printAreaHeight: number;

  comment: string | null;
  useSupports: boolean | null;
  wallThickness: number | null;
}
