from scrapy.loader import ItemLoader
from scrapy.loader.processors import TakeFirst, Join, Compose

#去重
class DbReLoader(ItemLoader):
    default_output_processor = TakeFirst()

class NewItemLoader(DbReLoader):
    text_out = Compose(Join(), lambda s: s.strip())
    source_out = Compose(Join(), lambda s: s.strip())


#不去重
class NoDbLoader(ItemLoader):
    text_out = Compose(Join(), lambda s: s.strip())
    source_out = Compose(Join(), lambda s: s.strip())
