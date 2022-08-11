import openpyxl


class ConfigReader:
    def __init__(self, path) -> None:
        self.wb = openpyxl.load_workbook(path)
        print("sheetnames:", self.wb.sheetnames)
        pass

    def outputJson(self, path):
        dicts = []
        for sheet in self.wb:
            dicts = self._createDict(sheet)

        pass

    def _createDict(self, sheet):
        attributes = []
        attribute_row = sheet[2]
        for cell in attribute_row:
            attributes.append(cell.value)
        
